using Microsoft.EntityFrameworkCore.Migrations;

namespace NomadDashboardsAPI.Migrations
{
    public partial class ExtentedUsermodelforEmployer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponyAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "AspNetUsers",
                newName: "Province");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "AspNetUsers",
                newName: "State");

            migrationBuilder.AddColumn<string>(
                name: "ComponyAddress",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                nullable: true);
        }
    }
}
