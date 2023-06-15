using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoSkola.Data.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "automobili",
                columns: table => new
                {
                    RegBroj = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_automobili", x => x.RegBroj);
                });

            migrationBuilder.CreateTable(
                name: "kategorije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "raspored",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumVreme = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raspored", x => x.Id);
                    table.ForeignKey(
                        name: "FK_raspored_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "kvar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumKvara = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AutomobilRegBroj = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kvar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kvar_automobili_AutomobilRegBroj",
                        column: x => x.AutomobilRegBroj,
                        principalTable: "automobili",
                        principalColumn: "RegBroj");
                });

            migrationBuilder.CreateTable(
                name: "userkategorija",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    KategorijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userkategorija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userkategorija_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userkategorija_kategorije_KategorijaId",
                        column: x => x.KategorijaId,
                        principalTable: "kategorije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "casovi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ocena = table.Column<float>(type: "real", nullable: false),
                    RasporedId = table.Column<int>(type: "int", nullable: false),
                    AutomobilRegBroj = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_casovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_casovi_automobili_AutomobilRegBroj",
                        column: x => x.AutomobilRegBroj,
                        principalTable: "automobili",
                        principalColumn: "RegBroj",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_casovi_raspored_RasporedId",
                        column: x => x.RasporedId,
                        principalTable: "raspored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userraspored",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RasporedId = table.Column<int>(type: "int", nullable: false),
                    Razlog = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumOdsustava = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userraspored", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userraspored_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userraspored_raspored_RasporedId",
                        column: x => x.RasporedId,
                        principalTable: "raspored",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_casovi_AutomobilRegBroj",
                table: "casovi",
                column: "AutomobilRegBroj");

            migrationBuilder.CreateIndex(
                name: "IX_casovi_RasporedId",
                table: "casovi",
                column: "RasporedId");

            migrationBuilder.CreateIndex(
                name: "IX_kvar_AutomobilRegBroj",
                table: "kvar",
                column: "AutomobilRegBroj");

            migrationBuilder.CreateIndex(
                name: "IX_raspored_UserId",
                table: "raspored",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userkategorija_KategorijaId",
                table: "userkategorija",
                column: "KategorijaId");

            migrationBuilder.CreateIndex(
                name: "IX_userkategorija_UserId",
                table: "userkategorija",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userraspored_RasporedId",
                table: "userraspored",
                column: "RasporedId");

            migrationBuilder.CreateIndex(
                name: "IX_userraspored_UserId",
                table: "userraspored",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "casovi");

            migrationBuilder.DropTable(
                name: "kvar");

            migrationBuilder.DropTable(
                name: "userkategorija");

            migrationBuilder.DropTable(
                name: "userraspored");

            migrationBuilder.DropTable(
                name: "automobili");

            migrationBuilder.DropTable(
                name: "kategorije");

            migrationBuilder.DropTable(
                name: "raspored");
        }
    }
}
