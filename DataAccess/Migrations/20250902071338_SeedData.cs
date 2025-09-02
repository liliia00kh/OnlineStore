using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bags" },
                    { 2, "Wallets" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, "Bag 1 description", "C:\\Users\\Liliia Malinska\\source\\repos\\OnlineStore\\OnlineStore\\wwwroot\\images\\bags\\bag-1.jpg", "Bag 1", 599.99m, 50 },
                    { 2, 1, "Bag 2 description", "C:\\Users\\Liliia Malinska\\source\\repos\\OnlineStore\\OnlineStore\\wwwroot\\images\\bags\\bag-2.jpg", "Bag 2", 999.99m, 30 },
                    { 3, 1, "Bag 3 description", "C:\\Users\\Liliia Malinska\\source\\repos\\OnlineStore\\OnlineStore\\wwwroot\\images\\bags\\bag-3.jpg", "Bag 3", 19.99m, 100 },
                    { 4, 1, "Bag 4 description", "C:\\Users\\Liliia Malinska\\source\\repos\\OnlineStore\\OnlineStore\\wwwroot\\images\\bags\\bag-4.jpg", "Bag 4", 12.99m, 70 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
