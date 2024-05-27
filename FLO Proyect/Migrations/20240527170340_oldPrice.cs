using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLOProyect.Migrations
{
    /// <inheritdoc />
    public partial class oldPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ColorToProduct_Products_CategoryId",
                table: "ColorToProduct");

            migrationBuilder.DropIndex(
                name: "IX_ColorToProduct_CategoryId",
                table: "ColorToProduct");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ColorToProduct");

            migrationBuilder.AddColumn<double>(
                name: "OldPrice",
                table: "Products",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldPrice",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ColorToProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ColorToProduct_CategoryId",
                table: "ColorToProduct",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ColorToProduct_Products_CategoryId",
                table: "ColorToProduct",
                column: "CategoryId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
