using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class slikazakategoriju : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Putanja",
                table: "kategorije",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Putanja",
                table: "kategorije");
        }
    }
}
