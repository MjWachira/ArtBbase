using ArtService.Models.Dtos;

namespace ArtService.Services.IServices
{
    public interface IUser
    {
        Task<UserDto> GetUserById(string Id);
    }
}
