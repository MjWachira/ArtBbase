using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;
using OrderService.Data;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Stripe;
using Stripe.Climate;
using ArtMessageBus;

namespace OrderService.Services
{
    public class OrdersService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly IUser _userService;
        private readonly IBid _bidService;
        private readonly IMessageBus _messageBus;

        public OrdersService(IUser userService, IBid bidService, ApplicationDbContext context,
            IMessageBus messageBus)
        {
            _userService = userService;
            _context = context;
            _bidService = bidService;
            _messageBus = messageBus;
           

        }

        public async Task<List<Orders>> GetAllOrders(Guid userId)
        {
            return await _context.Orders.Where(b=>b.BidderId == userId).ToListAsync();
        }
        //
        public async Task<Orders> GetOrderByBidId(Guid BidId)
        {
            return await _context.Orders.Where(b => b.BidId == BidId).FirstOrDefaultAsync();
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
                        Images = new List<string> {order.ArtImage}
                    }
                },
                Quantity = 1

            };

         
            options.LineItems.Add(item);
            
         //discount

         var DiscountObj = new List<SessionDiscountOptions>()
         {
             new SessionDiscountOptions()
             {
                 Coupon=order.CouponCode
             }
         };

         if (order.Discount > 0)
         {
             options.Discounts = DiscountObj;

         }
         
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

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {
            var order = await _context.Orders.Where(x => x.Id == OrderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                order.Status = "Paid";
                order.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();

                
                var user = await _userService.GetUserById(order.BidderId.ToString());

                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    return false;
                }
                else
                {
                    var email = new MailDto()
                    {
                        OrderId= order.Id,
                        OrderAmount=order.TotalAmount,
                        Name = user.Name,
                        Email = user.Email

                    };
                    await _messageBus.PublishMessage(email, "orderplaced");
                }

                // Send an Email to User
                //Reward the user with some Bonus Points 
                return true;
             
            }
            return false;
        }
    }
    
}
