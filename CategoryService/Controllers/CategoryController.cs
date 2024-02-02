using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CategoryService.Models.Dtos;
using CategoryService.Services.IServices;
using CategoryService.Models;
using Microsoft.AspNetCore.Authorization;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;
    
        public CategoryController( IMapper mapper, ICategory category)
        {
            _categoryService=category;
            _mapper=mapper;
            _response=new ResponseDto();
            
        }

        [HttpPost]
        [Authorize (Roles = "admin")]
        public async Task<ActionResult<ResponseDto>> AddCategory(AddCategoryDto newCategory)
        {
            var art = _mapper.Map<Category>(newCategory);
            var res = await _categoryService.AddCategory(art);
            _response.Result = res;
            return Created("", _response);
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAllCategories()
        {
            var res = await _categoryService.GetAllCategories();
            _response.Result = res;
            return Ok(_response);
        }

        [HttpGet ("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetCategory(Guid Id)
        {
            var res = await _categoryService.GetCategory(Id);
            _response.Result = res;
            return Ok(_response);
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto>> EdiArt(AddCategoryDto edit, Guid Id)
        {
            var category = await _categoryService.GetCategory(Id);

            if (category.CategoryName == null)
            {
                _response.Errormessage = "Category Not Found";
            }
            _mapper.Map(edit, category);

            var res = await _categoryService.UpdateCategory();
            _response.Result = res;
            return Created("", _response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto>> Delet(Guid Id)
        {
            var category = await _categoryService.GetCategory(Id);

            if (category.CategoryName == null)
            {
                _response.Errormessage = "Category Not Found";
            }

      
            var res = await _categoryService.DeleteCategory(category);
            _response.Result = res;
            return Created("", _response);
        }


    }
}
