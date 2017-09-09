using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booma.Database.Character.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "characters",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AccountId = table.Column<int>(nullable: false),
                    CharacterName = table.Column<string>(maxLength: 20, nullable: false),
                    CreationIp = table.Column<string>(maxLength: 15, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true),
                    isLoggedIn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.CharacterId);
                });

            migrationBuilder.CreateTable(
                name: "character_appearance",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    @class = table.Column<int>(name: "class", nullable: false),
                    SectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_appearance", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_character_appearance_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_appearance");

            migrationBuilder.DropTable(
                name: "characters");
        }
    }
}
