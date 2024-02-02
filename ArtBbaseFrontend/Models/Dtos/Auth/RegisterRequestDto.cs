using System.ComponentModel.DataAnnotations;

namespace ArtBbaseFrontend.Models.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]

        public string Email { get; set; } = string.Empty;

        public string? Image { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string CPassword { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Role { get; set; }

    }
}
