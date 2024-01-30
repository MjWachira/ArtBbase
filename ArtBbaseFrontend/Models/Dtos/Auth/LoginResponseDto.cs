namespace ArtBbaseFrontend.Models.Dtos.Auth
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;

        public string Token { get; set; } = string.Empty;
    }

}
