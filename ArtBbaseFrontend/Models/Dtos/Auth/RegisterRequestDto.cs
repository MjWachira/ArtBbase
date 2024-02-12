using System.ComponentModel.DataAnnotations;

namespace ArtBbaseFrontend.Models.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Full name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string? Image { get; set; } 
        public string? Role { get; set; }

    }
}
