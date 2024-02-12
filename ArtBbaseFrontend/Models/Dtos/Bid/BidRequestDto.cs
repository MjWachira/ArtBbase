namespace ArtBbaseFrontend.Models.Dtos.Bid
{
    public class BidRequestDto
    {
        public double BidAmmount { get; set; }
        public double HighestBid { get; set; }
        public Guid BidderId { get; set; }
    }
}
