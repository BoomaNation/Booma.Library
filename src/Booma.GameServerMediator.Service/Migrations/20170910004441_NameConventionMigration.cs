using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booma.GameServerMediator.Service.Migrations
{
    public partial class NameConventionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "claimed",
                table: "character_sessions",
                newName: "IsClaimed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsClaimed",
                table: "character_sessions",
                newName: "claimed");
        }
    }
}
