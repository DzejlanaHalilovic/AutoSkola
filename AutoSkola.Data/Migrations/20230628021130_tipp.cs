using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class tipp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorijaId",
                table: "automobili",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_automobili_KategorijaId",
                table: "automobili",
                column: "KategorijaId");

            migrationBuilder.AddForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili",
                column: "KategorijaId",
                principalTable: "kategorije",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_automobili_kategorije_KategorijaId",
                table: "automobili");

            migrationBuilder.DropIndex(
                name: "IX_automobili_KategorijaId",
                table: "automobili");

            migrationBuilder.DropColumn(
                name: "KategorijaId",
                table: "automobili");
        }
    }
}
