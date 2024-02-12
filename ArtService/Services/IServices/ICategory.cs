using ArtService.Models.Dtos;

namespace ArtService.Services.IServices
{
    public interface ICategory
    {
        Task<CategoryDto> GetCatById(string Id);
    }
}
