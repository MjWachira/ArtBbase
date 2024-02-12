using ArtBbaseFrontend.Models.Dtos.Bid;
using ArtBbaseFrontend.Models.Dtos;

namespace ArtBbaseFrontend.Services.Bid
{
    public interface IBid
    {
        Task<ResponseDto> AddBid(Guid Id,BidRequestDto bidRequestDto);
        Task<List<BidDto>> GetBidsAsync(Guid UserId);
        Task<List<BidDto>> GetMyHighestBidsAsync(string UserId);
        Task<List<BidDto>> GetMyWins(string UserId);
        Task<BidDto> GetHigestBidAsync(Guid ArtId);
        Task<ResponseDto> DeleteBid(Guid id);
        Task<ResponseDto> UpdateBid(Guid id, BidRequestDto bidRequestDto);
    }
}
