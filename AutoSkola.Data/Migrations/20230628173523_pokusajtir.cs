using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class pokusajtir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_AspNetUsers_InstruktorId",
                table: "automobili");

            migrationBuilder.DropIndex(
                name: "IX_automobili_InstruktorId",
                table: "automobili");

            migrationBuilder.DropColumn(
                name: "InstruktorId",
                table: "automobili");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstruktorId",
                table: "automobili",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_automobili_InstruktorId",
                table: "automobili",
                column: "InstruktorId");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_AspNetUsers_InstruktorId",
                table: "automobili",
                column: "InstruktorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
