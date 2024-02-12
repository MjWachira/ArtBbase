using CategoryService.Models;

namespace CategoryService.Services.IServices
{
    public interface ICategory
    {
        Task<string> AddCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategory(Guid Id);        
        Task<string> UpdateCategory();
        Task<string> DeleteCategory(Category category);
    }
}
