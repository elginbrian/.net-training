using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Deskripsi", "Durasi", "Genre", "JudulFilm" },
                values: new object[,]
                {
                    { 1, "Pencuri yang mencuri rahasia lewat mimpi.", 148, "Sci-Fi", "Inception" },
                    { 2, "Batman melawan ancaman Joker di Gotham.", 152, "Action", "The Dark Knight" },
                    { 3, "Perjalanan ruang angkasa demi menyelamatkan umat manusia.", 169, "Sci-Fi", "Interstellar" }
                });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "Id", "FasilitasTambahan", "Kapasitas", "NamaStudio" },
                values: new object[,]
                {
                    { 1, "Dolby Atmos, Recliner Seats", 50, "Studio 1 Premiere" },
                    { 2, "Layar IMAX 3D", 150, "Studio 2 IMAX" },
                    { 3, "AC, Kursi Standar", 100, "Studio 3 Reguler" }
                });

            migrationBuilder.InsertData(
                table: "JadwalFilms",
                columns: new[] { "Id", "FilmId", "HargaTiket", "StudioId", "WaktuTayang" },
                values: new object[,]
                {
                    { 1, 1, 75000m, 1, new DateTime(2026, 7, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 60000m, 2, new DateTime(2026, 7, 1, 16, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 50000m, 3, new DateTime(2026, 7, 1, 19, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JadwalFilms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JadwalFilms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JadwalFilms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Films",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
