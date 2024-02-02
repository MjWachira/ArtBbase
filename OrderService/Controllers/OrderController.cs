using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Migrations;
using OrderService.Models;
using OrderService.Models.Dtos;
using OrderService.Services.IServices;
using System.Security.Claims;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoupon _couponService;
        private readonly IBid _bidService;
        private readonly IArt _artService;
        private readonly IOrder _orderService;
        private readonly ResponseDto _responseDto;

        public OrderController(IMapper mapper, ICoupon coupon , IBid bid,  IArt art ,
            IOrder order )
        {
            _artService = art ;
            _mapper = mapper;
            _couponService = coupon;
            _bidService = bid;
            _orderService = order;
            _responseDto = new ResponseDto();
        }


        [HttpPost("{BidId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> PlaceOrder(MakeOrderDto dto, string BidId)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login";
                return Unauthorized(_responseDto);
            }

            var bid = await _bidService.GetBidById(BidId);


            if (string.IsNullOrWhiteSpace(bid.ArtName))
            {
                _responseDto.Errormessage = "Items Not Found";
                return NotFound(_responseDto);
            }


            var checkOrder = await _orderService.GetOrderByBidId(new Guid(BidId));
            if (checkOrder != null)
            {
                _responseDto.Errormessage = "Order already exists";
                return _responseDto;
            }


            var order = _mapper.Map<Orders>(dto);

            order.BidId = bid.Id;
            order.BidderId = new Guid(UserId);
            order.TotalAmount = bid.BidAmmount;
          



            var res = await _orderService.PlaceOrder(order);
            _responseDto.Result = res;
            return Ok(_responseDto);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetUserOrders()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _responseDto.Errormessage = "Please login";
                return Unauthorized(_responseDto);
            }

            var res = await _orderService.GetAllOrders(new Guid(UserId));
            _responseDto.Result = res;
            return Ok(_responseDto);


        }

        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetOneOrder(Guid Id)
        {

            var res = await _orderService.GetOrderById(Id);
            _responseDto.Result = res;
            return Ok(_responseDto);


        }
        [HttpPost("Pay")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> makePayments(StripeRequestDto dto)
        {

            var sR = await _orderService.MakePayments(dto);

            _responseDto.Result = sR;
            return Ok(_responseDto);
        }

        [HttpPost("validate/{Id}")]

        public async Task<ActionResult<ResponseDto>> validatePayment(Guid Id)
        {

            var res = await _orderService.ValidatePayments(Id);

            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }

            _responseDto.Errormessage = "Payment Failed!";
            return BadRequest(_responseDto);
        }

        [HttpPut("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(Guid Id, string Code)
        {
            
            var order = await _orderService.GetOrderById(Id);

            if (order == null)
            {
                _responseDto.Errormessage = "Order Not Found";
                return NotFound(_responseDto);
            }
            var coupon = await _couponService.GetCouponByCouponCode(Code);
            if (coupon.CouponAmount == null)
            {
                _responseDto.Errormessage = "Coupon is not Valid";
                return NotFound(_responseDto);
            }

            if (coupon.CouponMinAmount <= order.TotalAmount)
            {
                order.CouponCode = coupon.CouponCode;
                order.Discount = coupon.CouponAmount;
                await _orderService.saveChanges();
                _responseDto.Result = "Code applied";
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Errormessage = "Total amount is less that the minimum amount for this coupon";
                return BadRequest(_responseDto);
            }


            //return Ok(_responseDto);

        }
    }

}

