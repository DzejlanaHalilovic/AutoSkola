using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class nulll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userraspored_raspored_RasporedId",
                table: "userraspored");

            migrationBuilder.AlterColumn<int>(
                name: "RasporedId",
                table: "userraspored",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_userraspored_raspored_RasporedId",
                table: "userraspored",
                column: "RasporedId",
                principalTable: "raspored",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userraspored_raspored_RasporedId",
                table: "userraspored");

            migrationBuilder.AlterColumn<int>(
                name: "RasporedId",
                table: "userraspored",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_userraspored_raspored_RasporedId",
                table: "userraspored",
                column: "RasporedId",
                principalTable: "raspored",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
