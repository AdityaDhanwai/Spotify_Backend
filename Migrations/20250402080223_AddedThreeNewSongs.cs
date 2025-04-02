using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spotify_Backend_Assignment.Migrations
{
    /// <inheritdoc />
    public partial class AddedThreeNewSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "CoverImage", "FilePath", "Title", "UserId" },
                values: new object[,]
                {
                    { 5, "Ajit Parab", "/assets/images/faded.jpeg", "songs/Marathi Prarthana.mp3", "Marathi Prarthana", null },
                    { 6, "Arijit Singh", "/assets/images/Oonchi_Oonchi_Deewarein.jpg", "songs/Oonchi_Oonchi_Deewarein.mp3", "Oonchi Oonchi Deewarein", null },
                    { 7, "Lata Mangeshkar", "/assets/images/Ae Mere Watan Ke Logon.jpg", "songs/Ae Mere Watan Ke Logon.mp3", "Ae Mere Watan Ke Logon", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
