using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant.Migrations
{
    /// <inheritdoc />
    public partial class Mig1SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "FoodTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "پیش غذا" },
                    { 2, "غذای اصلی" },
                    { 3, "دسر" },
                    { 4, "نوشیدنی" }
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "ImageName", "Name", "Price", "Quantity", "TypeId" },
                values: new object[,]
                {
                    { 1, "سوپ جو با جو درست میشود", "soupJo", "سوپ جو", 120m, 20, 1 },
                    { 2, "در آن کشک و بادمجان وجود دارد", "KashkBademjan", "کشک بادمجان", 220m, 20, 1 },
                    { 3, "در آن میرزا و قاسم وجود دارد", "MirzaGhasemi", "میرزا قاسمی", 180m, 20, 1 },
                    { 4, "سالادی برای تمام فصول", "SeansonSalad", "سالاد فصل", 80m, 20, 1 },
                    { 5, "دو سیخ کباب", "KebabKoobide", "چلو کباب کوبیده", 360m, 20, 2 },
                    { 6, "کیکی که در آن شکلا وجود دارد", "ChoclateCake", "کیک شکلاتی", 110m, 20, 3 },
                    { 7, "دوغی که صنعتی نیست", "DooghMahali", "دوغ محلی", 39m, 20, 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
