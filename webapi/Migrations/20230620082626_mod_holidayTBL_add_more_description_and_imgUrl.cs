using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class mod_holidayTBL_add_more_description_and_imgUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description1",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description3",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description4",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description5",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl1",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl2",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl3",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl4",
                table: "Holidays",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl5",
                table: "Holidays",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description1",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Description3",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Description4",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "Description5",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgUrl1",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgUrl2",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgUrl3",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgUrl4",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "ImgUrl5",
                table: "Holidays");
        }
    }
}
