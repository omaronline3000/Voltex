using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Voltex.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "images",
                columns: new[] { "Id", "ImageName", "ProductId" },
                values: new object[,]
                {
                    { 1, "Test1", 1 },
                    { 2, "Test2", 1 },
                    { 3, "Test3", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "images",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
