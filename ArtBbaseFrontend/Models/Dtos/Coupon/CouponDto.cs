namespace ArtBbaseFrontend.Models.Dtos.Coupon
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; } = String.Empty;
        public int CouponAmount { get; set; }
        public int CouponMinAmount { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
