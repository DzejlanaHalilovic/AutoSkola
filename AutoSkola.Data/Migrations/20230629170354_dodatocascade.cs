using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class dodatocascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili",
                column: "KategorijaId",
                principalTable: "kategorije",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili",
                column: "KategorijaId",
                principalTable: "kategorije",
                principalColumn: "Id");
        }
    }
}
