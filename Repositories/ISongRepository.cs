using Spotify_Backend_Assignment.Models;

namespace Spotify_Backend_Assignment.Repositories
{
    public interface ISongRepository
    {
        Task<List<Song>> GetAllSongsAsync();
        Task<List<Song>> GetFavoriteSongsByUserIdAsync(string userId);

        Task<bool> IsSongFavoriteForUserAsync(string userId, int songId);

        Task RemoveFromFavoritesAsync(string userId, int songId);

        Task AddToFavoritesAsync(string userId, int songId);

    }
}
