﻿/*
    Intersect Game Engine (Server)
    Copyright (C) 2015  JC Snider, Joe Bridges
    
    Website: http://ascensiongamedev.com
    Contact Email: admin@ascensiongamedev.com 

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/

using System;
using Intersect_Library;
using Intersect_Library.GameObjects;
using Intersect_Server.Classes.General;
using Intersect_Server.Classes.Maps;
using Intersect_Server.Classes.Misc;
using Intersect_Server.Classes.Misc.Pathfinding;
using Intersect_Server.Classes.Networking;


namespace Intersect_Server.Classes.Entities
{
    public class Npc : Entity
    {
        //Targetting
        public Entity MyTarget = null;
        public NpcBase MyBase = null;

        //Pathfinding
        private Pathfinder pathFinder;

        //Temporary Values
        private int _curMapLink = -1;

        //Moving
        public long LastRandomMove;

        //Respawn
        public long RespawnTime;

        //Behaviour
        public byte Behaviour = 0;
        public byte Range = 0;

        public Npc(int index, NpcBase myBase)
            : base(index)
        {
            MyName = myBase.Name;
            MySprite = myBase.Sprite;
            MyBase = myBase;

            for (int I = 0; I < (int)Stats.StatCount; I++)
            {
                Stat[I] = new EntityStat(myBase.Stat[I]);
            }

            myBase.MaxVital.CopyTo(Vital, 0);
            myBase.MaxVital.CopyTo(MaxVital, 0);
            Behaviour = myBase.Behavior;
            Range = (byte)myBase.SightRange;
            pathFinder = new Pathfinder(this);
        }

        public override void Die(bool dropitems = false)
        {
            base.Die(dropitems);

            MapInstance.GetMap(CurrentMap).RemoveEntity(this);
            PacketSender.SendEntityLeave(MyIndex, (int)EntityTypes.GlobalEntity, CurrentMap);
            Globals.Entities[MyIndex] = null;
        }

        //Targeting
        public void AssignTarget(int Target)
        {
            if (Globals.Entities[Target].GetType() == typeof(Projectile))
            {
                MyTarget = ((Projectile)Globals.Entities[Target]).Owner;
            }
            else
            {
                MyTarget = Globals.Entities[Target];
            }
        }

        public bool CanAttack()
        {
            //Check if the attacker is stunned or blinded.
            for (var n = 0; n < Status.Count; n++)
            {
                if (Status[n].Type == (int)StatusTypes.Stun)
                {
                    return false;
                }
            }
            return true;
        }

        public override void Update()
        {
            base.Update();

            //Process dash spells
            if (Dashing != null)
            {
                Dashing.Update();
            }
            if (MoveTimer < Environment.TickCount)
            {
                var targetMap = -1;
                var targetX = 0;
                var targetY = 0;
                //Check if there is a target, if so, run their ass down.
                if (MyTarget != null)
                {
                    targetMap = MyTarget.CurrentMap;
                    targetX = MyTarget.CurrentX;
                    targetY = MyTarget.CurrentY;
                    for (var n = 0; n < MyTarget.Status.Count; n++)
                    {
                        if (MyTarget.Status[n].Type == (int)StatusTypes.Stealth)
                        {
                            targetMap = -1;
                            targetX = 0;
                            targetY = 0;
                        }
                    }
                }
                else //Find a target if able
                {
                    if (Behaviour == 1) // Check if attack on sight
                    {
                        int x = CurrentX - Range;
                        int y = CurrentY - Range;
                        int xMax = CurrentX + Range;
                        int yMax = CurrentY + Range;

                        //Check that not going out of the map boundaries
                        if (x < 0) x = 0;
                        if (y < 0) y = 0;
                        if (xMax >= Options.MapWidth) xMax = Options.MapWidth;
                        if (yMax >= Options.MapHeight) yMax = Options.MapHeight;

                        //TODO base this off of the entity array of surrounding maps, not the whole global list.
                        for (int n = 0; n < MapInstance.GetMap(CurrentMap).Entities.Count; n++)
                        {
                            if (MapInstance.GetMap(CurrentMap).Entities[n].GetType() == typeof(Player))
                            {
                                if (x < MapInstance.GetMap(CurrentMap).Entities[n].CurrentX && xMax > MapInstance.GetMap(CurrentMap).Entities[n].CurrentX)
                                {
                                    if (y < MapInstance.GetMap(CurrentMap).Entities[n].CurrentY && yMax > MapInstance.GetMap(CurrentMap).Entities[n].CurrentY)
                                    {
                                        // In range, so make a target
                                        MyTarget = MapInstance.GetMap(CurrentMap).Entities[n];
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }

                if (targetMap > -1)
                {
                    //Check if target map is on one of the surrounding maps, if not then we are not even going to look.
                    if (targetMap != CurrentMap)
                    {
                        if (MapInstance.GetMap(CurrentMap).SurroundingMaps.Count > 0)
                        {
                            for (var x = 0; x < MapInstance.GetMap(CurrentMap).SurroundingMaps.Count; x++)
                            {
                                if (MapInstance.GetMap(CurrentMap).SurroundingMaps[x] == targetMap)
                                {
                                    break;
                                }
                                if (x == MapInstance.GetMap(CurrentMap).SurroundingMaps.Count - 1)
                                {
                                    targetMap = -1;
                                }
                            }
                        }
                        else
                        {
                            targetMap = -1;
                        }
                    }
                }

                if (targetMap > -1)
                {
                    if (pathFinder.GetTarget() != null)
                    {
                        if (targetMap != pathFinder.GetTarget().TargetMap || targetX != pathFinder.GetTarget().TargetX || targetY != pathFinder.GetTarget().TargetY)
                        {
                            pathFinder.SetTarget(null);
                        }
                    }

                    if (pathFinder.GetTarget() != null)
                    {
                        if (!IsOneBlockAway(pathFinder.GetTarget().TargetMap, pathFinder.GetTarget().TargetX, pathFinder.GetTarget().TargetY))
                        {
                            var dir = pathFinder.GetMove();
                            if (dir > -1)
                            {
                                if (CanMove(dir) == -1 || CanMove(dir) == -4)
                                {
                                    //check if NPC is snared or stunned
                                    for (var n = 0; n < Status.Count; n++)
                                    {
                                        if (Status[n].Type == (int) StatusTypes.Stun ||
                                            Status[n].Type == (int) StatusTypes.Snare)
                                        {
                                            return;
                                        }
                                    }
                                    //Check if NPC is dashing
                                    if (Dashing != null)
                                    {
                                        return;
                                    }
                                    Move(dir, null);
                                    pathFinder.RemoveMove();
                                }
                                else
                                {
                                    pathFinder.SetTarget(null);
                                }
                            }
                        }
                        else
                        {
                            if (Dir != DirToEnemy(MyTarget.MyIndex) && DirToEnemy(MyTarget.MyIndex) != -1)
                            {
                                ChangeDir(DirToEnemy(MyTarget.MyIndex));
                            }
                            else
                            {
                                if (CanAttack()) TryAttack(MyTarget.MyIndex, null, -1, -1);
                            }
                        }
                    }
                    else
                    {
                        pathFinder.SetTarget(new PathfinderTarget(targetMap, targetX, targetY));
                        if (Dir != DirToEnemy(MyTarget.MyIndex) && DirToEnemy(MyTarget.MyIndex) != -1)
                        {
                            ChangeDir(DirToEnemy(MyTarget.MyIndex));
                        }
                        else
                        {
                            if (CanAttack()) TryAttack(MyTarget.MyIndex, null, -1, -1);
                        }
                    }
                }

                //Move randomly
                if (targetMap != -1) return;
                if (LastRandomMove >= Environment.TickCount) return;
                if (MyBase.Behavior == (int) NpcBehavior.Guard)
                {
                    LastRandomMove = Environment.TickCount + Globals.Rand.Next(1000, 3000);
                    return;
                }
                var i = Globals.Rand.Next(0, 1);
                if (i == 0)
                {
                    i = Globals.Rand.Next(0, 4);
                    if (CanMove(i) == -1)
                    {
                        //check if NPC is snared or stunned
                        for (var n = 0; n < Status.Count; n++)
                        {
                            if (Status[n].Type == (int)StatusTypes.Stun || Status[n].Type == (int)StatusTypes.Snare)
                            {
                                return;
                            }
                        }
                        //Check if NPC is dashing
                        if (Dashing != null)
                        {
                            return;
                        }
                        Move(i, null);
                    }
                }
                LastRandomMove = Environment.TickCount + Globals.Rand.Next(1000, 3000);
            }
            //If we switched maps, lets update the maps
            if (_curMapLink != CurrentMap)
            {
                if (_curMapLink != -1)
                {
                    MapInstance.GetMap(_curMapLink).RemoveEntity(this);
                }
                if (CurrentMap > -1)
                {
                    MapInstance.GetMap(CurrentMap).AddEntity(this);
                }
                _curMapLink = CurrentMap;
            }
        }
    }


}