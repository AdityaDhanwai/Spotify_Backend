using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spotify_Backend_Assignment.Models;
using Spotify_Backend_Assignment.Repositories;
using Spotify_Backend_Assignment.ViewModels;

namespace Spotify_Backend_Assignment.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public SongController(ISongRepository repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        ////[Authorize]
        //public async Task<IActionResult> Index()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var viewModel = new SongViewModel
        //    {
        //        AllSongs = await _repo.GetAllSongsAsync(),
        //        UserSongs = await _repo.GetFavoriteSongsByUserIdAsync(user.Id)
        //    };

        //    return View(viewModel);
        //}

        public async Task<IActionResult> Index()
        {
            var allSongs = await _repo.GetAllSongsAsync();
            List<Song> favoriteSongs = new();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                favoriteSongs = await _repo.GetFavoriteSongsByUserIdAsync(user.Id);
            }

            var viewModel = new SongViewModel
            {
                AllSongs = allSongs,
                UserSongs = favoriteSongs
            };

            return View(viewModel);
        }


        [HttpGet("api/allsongs")]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _repo.GetAllSongsAsync();
            return Json(songs);
        }

        [Authorize]
        [HttpGet("api/usersongs")]
        public async Task<IActionResult> GetUserFavoriteSongs()
        {
            var user = await _userManager.GetUserAsync(User);
            var favorites = await _repo.GetFavoriteSongsByUserIdAsync(user.Id);
            return Json(favorites);
        }

        [Authorize]
        [HttpGet("api/favoritesongs")]
        public async Task<IActionResult> GetFavoriteSongs()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var favorites = await _repo.GetFavoriteSongsByUserIdAsync(user.Id);
            return Json(favorites);
        }


        [Authorize]
        [HttpPost("api/favorite")]
        public async Task<IActionResult> AddToFavorites([FromBody] int songId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // ✅ Check if the song is already in user's favorites
            bool alreadyExists = await _repo.IsSongFavoriteForUserAsync(user.Id, songId);

            if (alreadyExists)
            {
                return Ok(new { success = false, message = "Song is already in your favorites." });
            }

            // ✅ If not already added, proceed to add
            await _repo.AddToFavoritesAsync(user.Id, songId);
            return Ok(new { success = true, message = "Added to favorites." });
        }



        [Authorize]
        [HttpPost("api/unfavorite")]
        public async Task<IActionResult> RemoveFromFavorites([FromBody] int songId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Check if it exists
            bool isFavorite = await _repo.IsSongFavoriteForUserAsync(user.Id, songId);
            if (!isFavorite)
            {
                return Ok(new { success = false, message = "This song is not in your favorites." });
            }

            // Proceed to remove
            await _repo.RemoveFromFavoritesAsync(user.Id, songId);
            return Ok(new { success = true, message = "Removed from favorites." });
        }

    }
}
