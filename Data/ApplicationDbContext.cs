using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spotify_Backend_Assignment.Models;

namespace Spotify_Backend_Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<FavoriteSong> FavoriteSongs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Song>().HasData(
                new Song
                {
                    Id = 1,
                    Title = "Hasi Ban Gaye",
                    Artist = "Shreya Goshal",
                    FilePath = "songs/Hasi_Ban_Gaye_Shreya.mp3",
                    CoverImage = "/assets/images/Hasi_Ban_Gaye_Shreya.jpg",
                    UserId = null // or provide a test user ID if needed
                },
                new Song
                {
                    Id = 2,
                    Title = "Perfect",
                    Artist = "Ed Sheeran",
                    FilePath = "songs/Perfect.mp3",
                    CoverImage = "/assets/images/Perfect.jpg",
                    UserId = null
                },
                new Song
                {
                    Id = 3,
                    Title = "Befikra",
                    Artist = "Aditi Singh Sharma",
                    FilePath = "songs/Befikra.mp3",
                    CoverImage = "/assets/images/Befikra.jpg",
                    UserId = null
                },
                new Song
                {
                    Id = 4,
                    Title = "Love You!",
                    Artist = "Ash King",
                    FilePath = "songs/BodyGuard_Love_You.mp3",
                    CoverImage = "/assets/images/BodyGuard_Love_You.jpg",
                    UserId = null
                }
            );
        }
    }
}
