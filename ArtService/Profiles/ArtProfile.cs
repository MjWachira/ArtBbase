using ArtService.Models;
using ArtService.Models.Dtos;
using AutoMapper;

namespace ArtService.Profiles
{
    public class ArtProfile:Profile
    {
        public ArtProfile()
        {
         CreateMap<AddArtDto, Art>().ReverseMap();
        }
    }
}
