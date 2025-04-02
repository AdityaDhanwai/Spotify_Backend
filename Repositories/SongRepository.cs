using Microsoft.EntityFrameworkCore;
using Spotify_Backend_Assignment.Data;
using Spotify_Backend_Assignment.Models;

namespace Spotify_Backend_Assignment.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly ApplicationDbContext _context;

        public SongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetAllSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<List<Song>> GetFavoriteSongsByUserIdAsync(string userId)
        {
            return await _context.FavoriteSongs
                .Where(f => f.UserId == userId)
                .Select(f => f.Song)
                .ToListAsync();
        }

        public async Task<bool> IsSongFavoriteForUserAsync(string userId, int songId)
        {
            return await _context.FavoriteSongs
                .AnyAsync(f => f.UserId == userId && f.SongId == songId);
        }
    

        public async Task AddToFavoritesAsync(string userId, int songId)
        {
            var alreadyFav = await _context.FavoriteSongs
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SongId == songId);

            if (alreadyFav == null)
            {
                var fav = new FavoriteSong
                {
                    UserId = userId,
                    SongId = songId
                };
                _context.FavoriteSongs.Add(fav);
                await _context.SaveChangesAsync();
            }
        }


        public async Task RemoveFromFavoritesAsync(string userId, int songId)
        {
            var favorite = await _context.FavoriteSongs
                .FirstOrDefaultAsync(f => f.UserId == userId && f.SongId == songId);

            if (favorite != null)
            {
                _context.FavoriteSongs.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

    }
}
