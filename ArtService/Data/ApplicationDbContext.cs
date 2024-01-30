using ArtService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Art> Arts { get; set; }
    }
}
