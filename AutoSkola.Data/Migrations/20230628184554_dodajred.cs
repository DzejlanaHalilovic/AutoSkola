using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class dodajred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAutomobil_AspNetUsers_InstruktorId",
                table: "UserAutomobil");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAutomobil_automobili_AutomobilId",
                table: "UserAutomobil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAutomobil",
                table: "UserAutomobil");

            migrationBuilder.RenameTable(
                name: "UserAutomobil",
                newName: "userAutomobil");

            migrationBuilder.RenameIndex(
                name: "IX_UserAutomobil_InstruktorId",
                table: "userAutomobil",
                newName: "IX_userAutomobil_InstruktorId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAutomobil_AutomobilId",
                table: "userAutomobil",
                newName: "IX_userAutomobil_AutomobilId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userAutomobil",
                table: "userAutomobil",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_userAutomobil_AspNetUsers_InstruktorId",
                table: "userAutomobil",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil",
                column: "AutomobilId",
                principalTable: "automobili",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userAutomobil_AspNetUsers_InstruktorId",
                table: "userAutomobil");

            migrationBuilder.DropForeignKey(
                name: "FK_userAutomobil_automobili_AutomobilId",
                table: "userAutomobil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userAutomobil",
                table: "userAutomobil");

            migrationBuilder.RenameTable(
                name: "userAutomobil",
                newName: "UserAutomobil");

            migrationBuilder.RenameIndex(
                name: "IX_userAutomobil_InstruktorId",
                table: "UserAutomobil",
                newName: "IX_UserAutomobil_InstruktorId");

            migrationBuilder.RenameIndex(
                name: "IX_userAutomobil_AutomobilId",
                table: "UserAutomobil",
                newName: "IX_UserAutomobil_AutomobilId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAutomobil",
                table: "UserAutomobil",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAutomobil_AspNetUsers_InstruktorId",
                table: "UserAutomobil",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAutomobil_automobili_AutomobilId",
                table: "UserAutomobil",
                column: "AutomobilId",
                principalTable: "automobili",
                principalColumn: "Id");
        }
    }
}
