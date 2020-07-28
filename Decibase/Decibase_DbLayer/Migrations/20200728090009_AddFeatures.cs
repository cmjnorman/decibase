using Microsoft.EntityFrameworkCore.Migrations;

namespace Decibase_DbLayer.Migrations
{
    public partial class AddFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId1",
                table: "TrackArtists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_TrackId1",
                table: "TrackArtists",
                column: "TrackId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId1",
                table: "TrackArtists",
                column: "TrackId1",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId1",
                table: "TrackArtists");

            migrationBuilder.DropIndex(
                name: "IX_TrackArtists_TrackId1",
                table: "TrackArtists");

            migrationBuilder.DropColumn(
                name: "TrackId1",
                table: "TrackArtists");
        }
    }
}
