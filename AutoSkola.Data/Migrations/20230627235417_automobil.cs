using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class automobil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_casovi_automobili_idauta",
                table: "casovi");

            migrationBuilder.DropIndex(
                name: "IX_casovi_idauta",
                table: "casovi");

            migrationBuilder.DropColumn(
                name: "idauta",
                table: "casovi");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idauta",
                table: "casovi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_casovi_idauta",
                table: "casovi",
                column: "idauta");

            migrationBuilder.AddForeignKey(
                name: "FK_casovi_automobili_idauta",
                table: "casovi",
                column: "idauta",
                principalTable: "automobili",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
