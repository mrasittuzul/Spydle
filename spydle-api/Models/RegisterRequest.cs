using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required]
        [Length(2, 32, ErrorMessage = "Username needs to be between 2 and 32 characters")]
        public string Username { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage = "Password needs to be at least 6 characters long.")]
        public string Password { get; set; } = null!;
    }
}
