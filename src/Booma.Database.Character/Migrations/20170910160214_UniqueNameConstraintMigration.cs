using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booma.Database.Character.Migrations
{
    public partial class UniqueNameConstraintMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "class",
                table: "character_appearance",
                newName: "Class");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_characters_CharacterName",
                table: "characters",
                column: "CharacterName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Class",
                table: "character_appearance",
                newName: "class");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_characters_CharacterName",
                table: "characters");
        }
    }
}
