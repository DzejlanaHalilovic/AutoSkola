using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class dodajuseraut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAutomobil",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstruktorId = table.Column<int>(type: "int", nullable: false),
                    AutomobilId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAutomobil", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserAutomobil_AspNetUsers_InstruktorId",
                        column: x => x.InstruktorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAutomobil_automobili_AutomobilId",
                        column: x => x.AutomobilId,
                        principalTable: "automobili",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAutomobil_AutomobilId",
                table: "UserAutomobil",
                column: "AutomobilId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAutomobil_InstruktorId",
                table: "UserAutomobil",
                column: "InstruktorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAutomobil");
        }
    }
}
