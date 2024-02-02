using ArtService.Models;
using ArtService.Models.Dtos;
using ArtService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
        private readonly IArt _artservice;
        /// <summary>
        /// 
        /// </summary>
        private readonly ICategory _catservice;

        public ArtController(ICategory catservice, IArt art, IMapper mapper)
        {
            _catservice = catservice;
            _mapper = mapper;
            _artservice = art;
            _response = new ResponseDto();
        }

        [HttpPost("Id")]
        [Authorize(Roles ="admin,seller")]
        public async Task<ActionResult<ResponseDto>> AddArt(AddArtDto newArt, string Id)
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to add art";
                return Unauthorized(_response);
            }

            var category =await _catservice.GetCatById(Id);
            if(string.IsNullOrEmpty(category.CategoryName))
            {
                _response.Errormessage = "Category Not Found";
                return NotFound(_response);
            }

            var art = _mapper.Map<Art>(newArt);
            art.SellerId = Guid.Parse(UserId);
            art.Category = category.CategoryName;
            var res = await _artservice.AddArt(art);
            _response.Result = res;
            return Created("", _response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllArts()
        {
            var arts = await _artservice.GetAllArts();
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("OpenArt")]
        public async Task<ActionResult<ResponseDto>> GetOpenArts()
        {
            var arts = await _artservice.GetOpenArts();
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("ClosedArts")]
        public async Task<ActionResult<ResponseDto>> GetClosedArts()
        {
            var arts = await _artservice.GetClosedArts();
            _response.Result = arts;
            return Ok(_response);
        }

        [HttpGet("myarts")]
        public async Task<ActionResult<ResponseDto>> GetMyArts()
        {
            var UserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UserId == null)
            {
                _response.Errormessage = "Please login to view your art";
                return Unauthorized(_response);
            }

            var arts = await _artservice.GetMyArts(Guid.Parse(UserId));
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("userArt{UserId}")]
        public async Task<ActionResult<ResponseDto>> UserArts(Guid UserId)
        {
  
            var arts = await _artservice.GetMyArts(UserId);
            _response.Result = arts;
            return Ok(_response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetOneArt(Guid Id)
        {
            //if(Id ==null) { }

            var art = await _artservice.GetOneArt(Id);
            _response.Result = art;
            return Ok(_response);
        }

        [HttpPut("{Id}")]
        [Authorize(Roles = "admin,seller")]
        public async Task<ActionResult<ResponseDto>> EdiArt(AddArtDto eArt, Guid Id)
        {
            var art = await _artservice.GetOneArt(Id);
            if (art == null)
            {
                _response.Errormessage = "Art Not Found";
            }

            _mapper.Map(eArt, art);

            var res = await _artservice.UpdateArt();
            _response.Result = res;
            return Created("", _response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "admin,seller")]
        public async Task<ActionResult<ResponseDto>> DeleteArt(Guid Id)
        {
            var art = await _artservice.GetOneArt(Id);
            if (art == null)
            {
                _response.Errormessage = "Art Not Found";
            }

            var res = await _artservice.DeleteArt(art);
            _response.Result = res;
            return Ok(_response);
        }
    }
}
