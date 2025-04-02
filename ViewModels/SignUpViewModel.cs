using System.ComponentModel.DataAnnotations;

namespace Spotify_Backend_Assignment.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^[a-zA-Z0-9_]{3,15}$", ErrorMessage = "Username must be 3-15 characters, no special symbols.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Password must be 6–16 characters.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,16}$", ErrorMessage = "Must contain 1 uppercase, 1 number, 1 special char.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
