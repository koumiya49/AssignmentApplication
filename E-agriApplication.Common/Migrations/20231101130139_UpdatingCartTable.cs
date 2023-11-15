using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApplication.Common.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "carts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "carts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
