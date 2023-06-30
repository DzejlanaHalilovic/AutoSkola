using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class dodatocascade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil");

            migrationBuilder.AddForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil",
                column: "AutomobilId",
                principalTable: "automobili",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil");

            migrationBuilder.AddForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil",
                column: "AutomobilId",
                principalTable: "automobili",
                principalColumn: "Id");
        }
    }
}
