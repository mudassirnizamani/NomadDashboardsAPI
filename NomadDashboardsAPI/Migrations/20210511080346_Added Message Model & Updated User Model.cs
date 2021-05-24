using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NomadDashboardsAPI.Migrations
{
    public partial class AddedMessageModelUpdatedUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MartialStatus",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    When = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserID",
                table: "Message",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.AddColumn<string>(
                name: "DOB",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MartialStatus",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
