using BidService.Data;
using BidService.Models;
using BidService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace BidService.Services
{
    public class BidsService : IBid
    {
        private readonly ApplicationDbContext _context;
        public BidsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddBid(Bid bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
            return "Bid was successful";
        }

        public async Task<List<Bid>> GetArBids(Guid ArtId)
        {
          return  await _context.Bids.Where(b=>b.ArtId== ArtId).ToListAsync();
        }

        public  async Task<List<Bid>> GetMyBids(Guid userId)
        {
            return await _context.Bids.Where(b => b.BidderId == userId).ToListAsync();
        }

        public async Task<List<Bid>> GetAllBids()
        {
            var bids = await _context.Bids.ToListAsync();

            foreach (var bid in bids)
            {
                if (DateTime.Now > bid.StopTime && bid.Status == "Open")
                {
                    bid.Status = "Closed";
                    _context.Entry(bid).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();

            return bids;
        }

        public async Task<List<Bid>> GetMyWins(Guid userId)
        {
            var highestBidAmounts = await _context.Bids
                .GroupBy(b => b.ArtId)
                .Select(g => g.Max(b => b.BidAmmount)) // Select the highest bid amount for each ArtId
                .ToListAsync();

            var openBids = await _context.Bids
                .Where(b => highestBidAmounts.Contains(b.BidAmmount) && b.BidderId == userId && !b.BidderId.Equals(Guid.Empty) && b.Status == "Closed")
                .ToListAsync();

            return openBids;
        }


        public async Task<Bid> GetOneBid(Guid Id)
        {
            return await _context.Bids.Where(b=>b.Id== Id).FirstOrDefaultAsync();    
        }

        public async Task<List<Bid>> HighestBidsPerItem(Guid userId)
        {
            var highestBids = await _context.Bids
                .Where(b => b.BidderId == userId) // Filter by the user ID
                .GroupBy(b => b.ArtId)
                .Select(g => g.OrderByDescending(b => b.BidAmmount).FirstOrDefault()) // Select the highest bid for each ArtId
                .ToListAsync();

            return highestBids;

        }
        public async Task<string> DeleteBid(Bid art)
        {
            _context.Bids.Remove(art);
            await _context.SaveChangesAsync();
            return "Bid removed successfully";
        }

        public async Task<List<Bid>> HighestBidsPerArt()
        {
            var highestBids = await _context.Bids
                .GroupBy(b => b.ArtId)
                .Select(g => g.OrderByDescending(b => b.BidAmmount).FirstOrDefault()) // Select the highest bid for each ArtId
                .ToListAsync();

            return highestBids;
        }
    }
}
