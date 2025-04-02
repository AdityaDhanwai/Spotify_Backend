using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Spotify_Backend_Assignment.Models
{
    public class FavoriteSong
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int SongId { get; set; }

        [ForeignKey("SongId")]
        public Song Song { get; set; }
    }
}
