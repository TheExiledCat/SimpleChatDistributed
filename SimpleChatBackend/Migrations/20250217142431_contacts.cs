using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SimpleChatBackend.Migrations
{
    /// <inheritdoc />
    public partial class contacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ChatRooms",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "ChatContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserAId = table.Column<int>(type: "integer", nullable: false),
                    UserBId = table.Column<int>(type: "integer", nullable: false),
                    ContactsSince = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrivateRoomId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatContacts_ChatRooms_PrivateRoomId",
                        column: x => x.PrivateRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatContacts_ChatUsers_UserAId",
                        column: x => x.UserAId,
                        principalTable: "ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatContacts_ChatUsers_UserBId",
                        column: x => x.UserBId,
                        principalTable: "ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatContacts_PrivateRoomId",
                table: "ChatContacts",
                column: "PrivateRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatContacts_UserAId",
                table: "ChatContacts",
                column: "UserAId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatContacts_UserBId",
                table: "ChatContacts",
                column: "UserBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatContacts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ChatRooms",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
