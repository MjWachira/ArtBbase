using AutoMapper;
using BidService.Models;
using BidService.Models.Dtos;
using BidService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BidService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IArt _artservice;
        private readonly IBid _bidservice;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        public BidController(IArt art, IBid bid, IMapper mapper)
        {
            _artservice = art;
            _bidservice = bid;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        [HttpPost("{Id}")]
        // [Authorize]
        public async Task<ActionResult<ResponseDto>> AddBid(AddBidDto addBid, string Id)
        {
            // var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            // if (UserId == null)
            // {
            //     _response.Errormessage = "Please login to make a bid";
            //     return Unauthorized(_response);
            // }

            var art = await _artservice.GetArtById(Id);
            if (string.IsNullOrWhiteSpace(art.Description))
            {
                _response.Errormessage = "Art Not Found";
                return NotFound(_response);
            }

            if (DateTime.Now > art.StopTime)
            {
                _response.Errormessage = "Bid Closed";
                return _response;
            }

            var bid = _mapper.Map<Bid>(addBid);
            bid.ArtId = art.Id;
            bid.ArtName = art.Name;
            bid.StopTime = art.StopTime;
            bid.ArtImage = art.Image;
            bid.HighestBid = addBid.BidAmmount;

            if (bid.BidAmmount < art.StartPrice)
            {
                _response.Errormessage = $"Current bid is Ksh{art.StartPrice}, make a higher bid";
                return _response;
            }

            // Check if the bid amount is less than or equal to the highest bid
            var highestBid = await _bidservice.GetArtHighBid(art.Id);

            if (bid.BidAmmount <= highestBid?.BidAmmount)
            {
                _response.Errormessage = $"Highest bid is Ksh{highestBid?.BidAmmount}, make a higher bid";
                return _response;
            }

            var res = await _bidservice.AddBid(bid);
            _response.Result = res;
            return Ok(_response);
        }

        [HttpGet("Art{Id}")]
        public async Task<ActionResult<ResponseDto>> ArtBids(string Id)
        {
            await _bidservice.GetAllBids();
            var bids = await _bidservice.GetArBids(new Guid(Id));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> AllBids()
        {
            var bids = await _bidservice.GetAllBids();
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("UserBids/{UserId}")]
       // [Authorize]
        public async Task<ActionResult<ResponseDto>> AllMyBids(Guid UserId)
        {
            await _bidservice.GetAllBids();
            // var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //var UserId = Id;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.GetMyBids(UserId);
            _response.Result = bids;
            return Ok(_response);
        }
        [HttpGet("AllHighestBids")]
        public async Task<ActionResult<ResponseDto>> ArtHighestBid()
        {
            await _bidservice.GetAllBids();
            var bids = await _bidservice.HighestBidsPerArt();
            _response.Result = bids;
            return Ok(_response);
        }
        [HttpGet("MyHighestBids/{UserId}")]
       // [Authorize]
        public async Task<ActionResult<ResponseDto>> HighestBid( Guid UserId)
        {
            await _bidservice.GetAllBids();
           // var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to make a bid";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.HighestBidsPerItem((UserId));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("MyWins/{UserId}")]
        //[Authorize]
        public async Task<ActionResult<ResponseDto>> MyWins(string UserId)
        {
            await _bidservice.GetAllBids();
            //var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login";
                return Unauthorized(_response);
            }
            var bids = await _bidservice.GetMyWins(new Guid(UserId));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> OneBid(string Id)
        {
             await _bidservice.GetAllBids();
            var bids = await _bidservice.GetOneBid(new Guid(Id));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpGet("Art/{Id}")]
        public async Task<ActionResult<ResponseDto>> OneHighBid(string Id)
        {
            await _bidservice.GetAllBids();
            var bids = await _bidservice.GetArtHighBid(new Guid(Id));
            _response.Result = bids;
            return Ok(_response);
        }

        [HttpDelete("{Id}")]
        //[Authorize]
        public async Task<ActionResult<ResponseDto>> DeleteBid(Guid Id)
        {
            var bid = await _bidservice.GetOneBid(Id);
            if (bid == null)
            {
                _response.Errormessage = "Bid Not Found";
            }

            var res = await _bidservice.DeleteBid(bid);
            _response.Result = res;
            return Ok(_response);
        }
    }
}
