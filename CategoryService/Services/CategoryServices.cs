using CategoryService.Data;
using CategoryService.Models;
using CategoryService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Services
{
    public class CategoryServices : ICategory
    {
        private readonly ApplicationDbContext _context;
        public CategoryServices( ApplicationDbContext context)
        {
            _context = context;   
        }
        public async Task<string> AddCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();
            return "Category Added Successfully";
        }

        public async Task<string> DeleteCategory(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return "Category Removed";
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category> GetCategory(Guid Id)
        {
            return await _context.Category.Where(b=>b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateCategory()
        {
            await _context.SaveChangesAsync();
            return "Category Updated !!";
        }
    }
}
