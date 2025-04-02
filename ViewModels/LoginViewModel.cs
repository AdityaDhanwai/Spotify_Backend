using System.ComponentModel.DataAnnotations;

namespace Spotify_Backend_Assignment.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email or Username is required")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\W).+$",
     ErrorMessage = "Password must contain at least one uppercase letter and one special character.")]
        public string Password { get; set; }
    }
}
