using System.ComponentModel.DataAnnotations;

namespace ArtBbaseFrontend.Models.Dtos.Coupon
{
    public class CouponRequestDto
    {
        [Required]
        public string CouponCode { get; set; } = String.Empty;
        [Required]
        [Range(100, 100000)]
        public int CouponAmount { get; set; }
        [Required]
        public int CouponMinAmount { get; set; }
    }
}
