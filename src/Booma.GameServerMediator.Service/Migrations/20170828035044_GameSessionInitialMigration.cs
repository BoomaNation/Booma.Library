using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booma
{
    public partial class GameSessionInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SessionCreationTime = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAddOrUpdate", true),
                    SessionGuid = table.Column<Guid>(nullable: false),
                    SessionIp = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_GameSessions_AvatarEntries_UserId",
                        column: x => x.UserId,
                        principalTable: "AvatarEntries",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSessions");
        }
    }
}
