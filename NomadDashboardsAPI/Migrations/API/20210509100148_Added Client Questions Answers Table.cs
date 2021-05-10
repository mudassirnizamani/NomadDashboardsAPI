using Microsoft.EntityFrameworkCore.Migrations;

namespace NomadDashboardsAPI.Migrations.API
{
    public partial class AddedClientQuestionsAnswersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientQuestionsAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_1_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_2_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_3_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_4_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_5_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_6_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_7_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_8_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question_9_Answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientQuestionsAnswers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientQuestionsAnswers");
        }
    }
}
