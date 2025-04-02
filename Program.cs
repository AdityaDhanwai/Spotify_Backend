using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spotify_Backend_Assignment.Data;
using Spotify_Backend_Assignment.Models;
using Spotify_Backend_Assignment.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<ISongRepository, SongRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
 
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Song}/{action=Index}/{id?}");
async Task SeedTestUserAsync()
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    var user = await userManager.FindByEmailAsync("test@spotify.com");
    if (user == null)
    {
        var newUser = new ApplicationUser
        {
            UserName = "testuser",
            Email = "test@spotify.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(newUser, "Test@123");
    }
}

await SeedTestUserAsync();

app.Run();
