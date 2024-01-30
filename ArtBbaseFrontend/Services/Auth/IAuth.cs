using ArtBbaseFrontend.Models.Dtos;
using ArtBbaseFrontend.Models.Dtos.Auth;

namespace ArtBbaseFrontend.Services.Auth
{
    public interface IAuth
    {
        Task<ResponseDto> Register(RegisterRequestDto registerRequestDto);

        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
