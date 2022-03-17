using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scot.Services.ProductAPI.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Meal", "Tipycal Scottish meal. From Highlands", "https://microserviceproject.blob.core.windows.net/scot/haggis.jpg", "Haggis", 7.9900000000000002 },
                    { 2, "Meal", "Another typical scottish meal. From... well, could be anywhere :D", "https://microserviceproject.blob.core.windows.net/scot/mac-and-cheese.png", "Mac & Cheese", 14.75 },
                    { 3, "Meal", "Pizza with cheese, tomato, basil and haggis. Yum yum!", "https://microserviceproject.blob.core.windows.net/scot/pizza_haggis.png", "Pizza with Haggis", 10.25 },
                    { 4, "Meal", "Directly from her Majesty The Queen, the biggest hamburger! Only for faggots", "https://microserviceproject.blob.core.windows.net/scot/royal_hamburger.jpg", "Royal Hamburger", 16.890000000000001 },
                    { 5, "Side", "French. Fries.", "https://microserviceproject.blob.core.windows.net/scot/French_Fries.jpg", "French fries", 6.5499999999999998 },
                    { 6, "Side", "Mixture of Snacks", "https://microserviceproject.blob.core.windows.net/scot/snacks.jpg", "Snaks", 3.9500000000000002 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);
        }
    }
}
