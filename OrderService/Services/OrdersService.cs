using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;
using OrderService.Data;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

namespace OrderService.Services
{
    public class OrdersService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly IUser _userService;
        private readonly IBid _bidService;
        //private readonly IMessageBus _messageBUs;

        public OrdersService(IUser userService, IBid bidService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
            _bidService = bidService;

        }

        public async Task<List<Orders>> GetAllOrders(Guid userId)
        {
            return await _context.Orders.Where(b=>b.BidderId == userId).ToListAsync();
        }

        public async Task<Orders> GetOrderById(Guid Id)
        {
            return await _context.Orders.Where(b=> b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Orders.Where(x => x.Id == stripeRequestDto.OrderId).FirstOrDefaultAsync();
            var bid = await _bidService.GetBidById((order.BidId).ToString());

            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };



            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)order.TotalAmount * 100,
                    Currency = "kes",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = bid.ArtName,
                        Images = new List<string> { "https://imgs.search.brave.com/av4uh1BAXrv7q2gkJt-E709vrIz3mB1-wrcPDtDyZNI/rs:fit:500:0:0/g:ce/aHR0cHM6Ly93d3cu/ZXhwZXJ0YWZyaWNh/LmNvbS9pbWFnZXMv/YXJlYS8xODI5X2wu/anBn" }
                    }
                },
                Quantity = 1


            };

            /*
            options.LineItems.Add(item);

            //discount

            var DiscountObj = new List<SessionDiscountOptions>()
            {
                new SessionDiscountOptions()
                {
                    Coupon=booking.CouponCode
                }
            };

            if (booking.Discount > 0)
            {
                options.Discounts = DiscountObj;

            }
            */
            var service = new SessionService();
            Session session = service.Create(options);

            // URL//ID

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            //update Database =>status/ SessionId 

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }

        public async Task<string> PlaceOrder(Orders order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return "Order Placed Successfully";
        }

        public Task saveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidatePayments(Guid OrderId)
        {
            throw new NotImplementedException();
        }
    }
}
