using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Category { get; set; }
    }
}
