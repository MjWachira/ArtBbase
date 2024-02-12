using OrderService.Models;
using OrderService.Models.Dtos;

namespace OrderService.Services.IServices
{
    public interface IOrder
    {
        Task<string> PlaceOrder(Orders order);
        Task saveChanges();
        Task<List<Orders>> GetAllOrders(Guid userId);
        Task<Orders> GetOrderById(Guid Id);
        Task<Orders> GetOrderByBidId(Guid BidId);
        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);
        Task<bool> ValidatePayments(Guid OrderId);
    }
}
