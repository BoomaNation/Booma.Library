using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booma.GameServerMediator.Service.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_sessions",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SessionCreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true),
                    SessionGuid = table.Column<Guid>(nullable: false),
                    SessionIp = table.Column<string>(maxLength: 15, nullable: true),
                    claimed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_sessions", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_character_sessions_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_sessions");
        }
    }
}
