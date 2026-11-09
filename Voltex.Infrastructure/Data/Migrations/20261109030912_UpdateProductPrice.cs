using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voltex.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "products",
                newName: "OldPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "NewPrice",
                table: "products",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1,
                column: "NewPrice",
                value: 100.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewPrice",
                table: "products");

            migrationBuilder.RenameColumn(
                name: "OldPrice",
                table: "products",
                newName: "Price");
        }
    }
}
