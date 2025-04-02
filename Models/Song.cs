namespace Spotify_Backend_Assignment.Models
{
    public class Song
{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string FilePath { get; set; }
        public string CoverImage { get; set; }

        //Foreign key
        public string? UserId { get; set; }

        // Navigation property
        public ApplicationUser? User { get; set; }
    }

}
