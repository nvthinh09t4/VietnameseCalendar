using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class mod_holidayTBL_add_more_imgCaptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgCaption1",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgCaption2",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgCaption3",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgCaption4",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgCaption5",
                table: "Holidays",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgCaption1",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgCaption2",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgCaption3",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgCaption4",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgCaption5",
                table: "Holidays");
        }
    }
}
