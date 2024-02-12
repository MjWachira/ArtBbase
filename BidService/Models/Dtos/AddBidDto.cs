namespace BidService.Models.Dtos
{
    public class AddBidDto
    {
        public Guid BidderId { get; set; }
        public double BidAmmount { get; set; }
    }
}
