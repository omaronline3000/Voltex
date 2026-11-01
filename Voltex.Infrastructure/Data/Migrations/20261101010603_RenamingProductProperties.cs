using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voltex.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamingProductProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "products",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "products",
                newName: "ProductName");
        }
    }
}
