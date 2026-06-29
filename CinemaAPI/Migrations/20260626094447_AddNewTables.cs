using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JadwalFilms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudioId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<int>(type: "int", nullable: false),
                    WaktuTayang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HargaTiket = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JadwalFilms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JadwalFilms_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JadwalFilms_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tikets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JadwalFilmId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomorKursi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusTiket = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tikets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tikets_JadwalFilms_JadwalFilmId",
                        column: x => x.JadwalFilmId,
                        principalTable: "JadwalFilms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaksis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiketId = table.Column<int>(type: "int", nullable: false),
                    MetodePembayaran = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusPembayaran = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaksis_Tikets_TiketId",
                        column: x => x.TiketId,
                        principalTable: "Tikets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JadwalFilms_FilmId",
                table: "JadwalFilms",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_JadwalFilms_StudioId",
                table: "JadwalFilms",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tikets_JadwalFilmId",
                table: "Tikets",
                column: "JadwalFilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksis_TiketId",
                table: "Transaksis",
                column: "TiketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaksis");

            migrationBuilder.DropTable(
                name: "Tikets");

            migrationBuilder.DropTable(
                name: "JadwalFilms");
        }
    }
}
