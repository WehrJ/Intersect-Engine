using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using Intersect.Enums;
using Intersect.Server.Entities;


namespace Intersect.Server.Database.PlayerData.Players
{
    /// <summary>
    /// A class containing the definition of each faction, alongside the methods to use them.
    /// </summary>
    public class Faction
    {

        public Faction()
        {
        }

        public Faction(Player faction, string name)
        {
    Name = name;
    FoundingDate = DateTime.Now;
            }

        /// <summary>
        /// The database Id of the faction.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the faction.
        /// </summary>
        public string Name { get; set; }

        public DateTime FoundingDate { get; set; }

        /// <summary>
         /// Contains a record of all guild members
         /// </summary>
        public List<Player> Members { get; set; } = new List<Player>();

        /// <summary>
        /// Search for a faction by Id.
        /// </summary>
        /// <param name="context">The playercontext to search through.</param>
        /// <param name="id">The faction Id to search for.</param>
        /// <returns>Returns a <see cref="Faction"/> that matches the Id, if any.</returns>
        public static Faction GetGuild(PlayerContext context, Guid id)
        {
    var faction = context.Factions.Where(p => p.Id == id).SingleOrDefault();
    
                return faction;
            }

        /// <summary>
        /// Search for a faction by Name.
        /// </summary>
        /// <param name="context">The playercontext to search through.</param>
        /// <param name="name">The faction Name to search for.</param>
        /// <returns>Returns a <see cref="Faction"/> that matches the Name, if any.</returns>
        public static Faction GetGuild(PlayerContext context, string name)
        {
    var faction= context.Factions.Where(p => p.Name == name).SingleOrDefault();
    
                return faction;
            }
    }
}