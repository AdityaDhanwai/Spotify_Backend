using Spotify_Backend_Assignment.Models;

namespace Spotify_Backend_Assignment.ViewModels
{
    public class SongViewModel
    {
        public List<Song> AllSongs { get; set; }
        public List<Song> UserSongs { get; set; }
    }
}
