namespace ArtBbaseFrontend.Models.Dtos.Order
{
    public class OrdeDto
    {
        public Guid Id { get; set; }
        public Guid BidId { get; set; }
        public Guid BidderId { get; set; }
        public Guid ArtId { get; set; }
        public DateTime DatePlaced { get; set; } = DateTime.Now;
        public string? ArtName { get; set; }
        public string ArtImage { get; set; } = string.Empty;
        public string CouponCode { get; set; } = string.Empty;
        public double Discount { get; set; }
        public Double TotalAmount { get; set; }
        public string? StripeSessionId { get; set; }
        public string Status { get; set; } = "Pending";
        public string PaymentIntent { get; set; } = string.Empty;

    }
}
