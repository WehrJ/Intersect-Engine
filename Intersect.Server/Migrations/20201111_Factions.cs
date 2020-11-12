using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intersect.Server.Migrations
{
    public partial class Factions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FactionId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Factions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FoundingDate = table.Column<DateTime>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factions", x => x.Id);
                });

migrationBuilder.CreateIndex(
name: "IX_Players_FactionId",
table: "Players",
column: "FactionId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
    
    migrationBuilder.DropTable(
    name: "Factions");
    
    migrationBuilder.DropIndex(
    name: "IX_Players_FactionId",
    table: "Players");
    
    migrationBuilder.DropColumn(
    name: "FactionId",
    table: "Players");
            }
    }
}
