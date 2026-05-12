using System.ComponentModel.DataAnnotations;

namespace spydle_api.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
