using ArtService.Data;
using ArtService.Models;
using ArtService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtService.Services
{
    public class ArtsService : IArt
    {
        private readonly ApplicationDbContext _context;
        public ArtsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddArt(Art art)
        {
            _context.Arts.Add(art);
            await _context.SaveChangesAsync();
            return "Art Gallery Added Successfully";
        }

        public async Task<string> DeleteArt(Art art)
        {
            _context.Arts.Remove(art);
            await _context.SaveChangesAsync();
            return "art removed successfully";
        }

        public async Task<List<Art>> GetAllArts()
        {
            var arts = await _context.Arts.ToListAsync();
            foreach (var art in arts)
            {
                if (art.StopTime < DateTime.Now && art.Status == "Open")
                {
                    art.Status = "Closed";
                }
            }
            await _context.SaveChangesAsync();
            return arts;
        }

        public async Task<Art> GetOneArt(Guid Id)
        {
            var art = await _context.Arts.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (art != null && art.StopTime < DateTime.Now && art.Status == "Open")
            {
                art.Status = "Closed";
                await _context.SaveChangesAsync(); 
            }
            return art;
        }


        public async Task<List<Art>> GetMyArts(Guid userId)
        {
            return await _context.Arts.Where(x => x.SellerId == userId).ToListAsync();
        }

        public async Task<string> UpdateArt()
        {
            await _context.SaveChangesAsync();
            return "Art updated successfully";
        }

        public async Task<List<Art>> GetOpenArts()
        {
            return await _context.Arts.Where(x=>x.Status =="Open").ToListAsync();
        }

        public async Task<List<Art>> GetClosedArts()
        {
            return await _context.Arts.Where(x => x.Status == "Closed").ToListAsync();
        }
    }
}
