namespace ArtBbaseFrontend.Models.Dtos.Bid
{
    public class BidDto
    {
        public Guid Id { get; set; }
        public Guid ArtId { get; set; }
        public string ArtImage { get; set; }
        public string? ArtName { get; set; }
        public Guid BidderId { get; set; }
        public string? BidderName { get; set; }
        public double BidAmmount { get; set; } = 0;
        public double HighestBid { get; set; } = 0;
        public DateTime BidDate { get; set; } = DateTime.Now;
        public DateTime StopTime { get; set; }
        public string Status { get; set; } = "Open";
    }
}
