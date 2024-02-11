using ArtBbaseFrontend.Models.Dtos.Order;
using ArtBbaseFrontend.Models.Dtos;

namespace ArtBbaseFrontend.Services.Order
{
    public interface IOrder
    {
        Task<ResponseDto> PlaceOrder(Guid Id, OrderRequestDto orderRequestDto);
        Task<List<OrdeDto>> UserOrdersAsync(string UserId);
        Task<OrdeDto> GetOrderByIdAsync(Guid id);
        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid OrderId);
    }
}
