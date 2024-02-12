using AutoMapper;
using CategoryService.Models;
using CategoryService.Models.Dtos;

namespace CategoryService.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<AddCategoryDto, Category>().ReverseMap();
        }
    }
}
