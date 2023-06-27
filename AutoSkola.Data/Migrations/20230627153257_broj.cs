using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class broj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_InstruktorId",
                table: "polaznikinstuktor");

            migrationBuilder.DropForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_PolaznikId",
                table: "polaznikinstuktor");

            migrationBuilder.DropForeignKey(
                name: "FK_raspored_AspNetUsers_InstruktorId",
                table: "raspored");

            migrationBuilder.DropForeignKey(
                name: "FK_raspored_AspNetUsers_PolaznikId",
                table: "raspored");

            migrationBuilder.AddColumn<int>(
                name: "BrojCasova",
                table: "polaznikinstuktor",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_InstruktorId",
                table: "polaznikinstuktor",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_PolaznikId",
                table: "polaznikinstuktor",
                column: "PolaznikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_raspored_AspNetUsers_InstruktorId",
                table: "raspored",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_raspored_AspNetUsers_PolaznikId",
                table: "raspored",
                column: "PolaznikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_InstruktorId",
                table: "polaznikinstuktor");

            migrationBuilder.DropForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_PolaznikId",
                table: "polaznikinstuktor");

            migrationBuilder.DropForeignKey(
                name: "FK_raspored_AspNetUsers_InstruktorId",
                table: "raspored");

            migrationBuilder.DropForeignKey(
                name: "FK_raspored_AspNetUsers_PolaznikId",
                table: "raspored");

            migrationBuilder.DropColumn(
                name: "BrojCasova",
                table: "polaznikinstuktor");

            migrationBuilder.AddForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_InstruktorId",
                table: "polaznikinstuktor",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_polaznikinstuktor_AspNetUsers_PolaznikId",
                table: "polaznikinstuktor",
                column: "PolaznikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_raspored_AspNetUsers_InstruktorId",
                table: "raspored",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_raspored_AspNetUsers_PolaznikId",
                table: "raspored",
                column: "PolaznikId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
