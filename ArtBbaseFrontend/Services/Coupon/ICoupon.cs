using ArtBbaseFrontend.Models.Dtos.Coupon;
using ArtBbaseFrontend.Models.Dtos;

namespace ArtBbaseFrontend.Services.Coupon
{
    public interface ICoupon
    {
        Task<List<CouponDto>> GetCouponsAsync();

        Task<CouponDto> GetCouponAsync(string code);

        Task<ResponseDto> AddCoupon(CouponRequestDto couponRequestDto);

        Task<ResponseDto> DeleteCoupon(Guid id);

        Task<ResponseDto> UpdateCoupon(Guid id, CouponRequestDto couponRequestDto);
    }
}

