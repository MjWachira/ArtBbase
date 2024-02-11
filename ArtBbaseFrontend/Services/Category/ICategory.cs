using ArtBbaseFrontend.Models.Dtos.Category;
using ArtBbaseFrontend.Models.Dtos;

namespace ArtBbaseFrontend.Services.Category
{
    public interface ICategory
    {
        Task<List<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto> GetCategoryAsync(Guid Id);

        Task<ResponseDto> AddCategory(CategoryRequestDto categoryRequestDto);

        Task<ResponseDto> DeleteCategory(Guid id);

        Task<ResponseDto> UpdateCategory(Guid id, CategoryRequestDto categoryRequestDto);
    }
}
