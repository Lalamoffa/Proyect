using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FLOProyect.Migrations
{
    /// <inheritdoc />
    public partial class SliderIscheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ischeck",
                table: "Sliders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ischeck",
                table: "Sliders");
        }
    }
}
