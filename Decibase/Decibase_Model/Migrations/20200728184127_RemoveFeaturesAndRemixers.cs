using Microsoft.EntityFrameworkCore.Migrations;

namespace Decibase_Model.Migrations
{
    public partial class RemoveFeaturesAndRemixers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId1",
                table: "TrackArtists");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId2",
                table: "TrackArtists");

            migrationBuilder.DropIndex(
                name: "IX_TrackArtists_TrackId1",
                table: "TrackArtists");

            migrationBuilder.DropIndex(
                name: "IX_TrackArtists_TrackId2",
                table: "TrackArtists");

            migrationBuilder.DropColumn(
                name: "TrackId1",
                table: "TrackArtists");

            migrationBuilder.DropColumn(
                name: "TrackId2",
                table: "TrackArtists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackId1",
                table: "TrackArtists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrackId2",
                table: "TrackArtists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_TrackId1",
                table: "TrackArtists",
                column: "TrackId1");

            migrationBuilder.CreateIndex(
                name: "IX_TrackArtists_TrackId2",
                table: "TrackArtists",
                column: "TrackId2");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId1",
                table: "TrackArtists",
                column: "TrackId1",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackArtists_Tracks_TrackId2",
                table: "TrackArtists",
                column: "TrackId2",
                principalTable: "Tracks",
                principalColumn: "TrackId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
