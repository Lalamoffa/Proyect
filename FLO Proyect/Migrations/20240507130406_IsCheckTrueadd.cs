using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLOProyect.Migrations
{
    /// <inheritdoc />
    public partial class IsCheckTrueadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ischeck",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Ischeck",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ischeck",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ischeck",
                table: "Categories");
        }
    }
}
