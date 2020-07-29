using Microsoft.EntityFrameworkCore.Migrations;

namespace Decibase_Model.Migrations
{
    public partial class ChangeAlbumNametoTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Albums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
