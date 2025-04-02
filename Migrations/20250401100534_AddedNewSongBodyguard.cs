using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spotify_Backend_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewSongBodyguard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CoverImage", "FilePath", "Title", "UserId" },
                values: new object[] { 4, "Ash King", "/assets/images/BodyGuard_Love_You.jpg", "songs/BodyGuard_Love_You.mp3", "Love You!", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
