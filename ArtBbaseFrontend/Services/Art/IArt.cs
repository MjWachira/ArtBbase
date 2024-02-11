using ArtBbaseFrontend.Models.Dtos.Art;
using ArtBbaseFrontend.Models.Dtos;

namespace ArtBbaseFrontend.Services.Art
{
    public interface IArt
    {
        Task<List<ArtDto>> GetArtsAsync();
        Task<ArtDto> GetArtByIdAsync(Guid id);
        Task<List<ArtDto>> GetOpenArtsAsync();
        Task<List<ArtDto>> GetClosedArtsAsync();
        Task<List<ArtDto>> GetUserArtsAsync(Guid UserId);
        Task<ResponseDto> AddArt(ArtRequestDto product);
        Task<ResponseDto> DeleteArt(Guid id);
        Task<ResponseDto> UpdateArt(Guid id, ArtRequestDto productRequestDto);

        Task<ResponseDto> UpdateHighestBid(Guid artId, double newHighestBid);
    }
}
