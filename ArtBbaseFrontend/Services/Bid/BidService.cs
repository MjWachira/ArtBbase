using ArtBbaseFrontend.Models.Dtos;
using ArtBbaseFrontend.Models.Dtos.Bid;
using ArtBbaseFrontend.Models.Dtos.Category;
using Newtonsoft.Json;
using System.Text;

namespace ArtBbaseFrontend.Services.Bid
{
    public class BidService : IBid
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl = "https://localhost:7259";

        private readonly string _baseUrl = "https://artbidservice.azurewebsites.net";

        public BidService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ResponseDto> AddBid(Guid Id, BidRequestDto bidRequestDto)
        {
            var request = JsonConvert.SerializeObject(bidRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Bid/{Id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of categories
                return results;
            }
            return new ResponseDto();
        }

        public async Task<ResponseDto> DeleteBid(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Bid/{id}");
            var content = await response.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(content))
            {
                var results = JsonConvert.DeserializeObject<ResponseDto>(content);
                if (results != null && results.IsSuccess)
                {
                    return results;
                }
            }

            return new ResponseDto();
        }

        public async Task<List<BidDto>> GetBidsAsync( Guid UserId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Bid/UserBids/{UserId}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<BidDto>>(results.Result.ToString());
            }
            return new List<BidDto>();
        }

        public async Task<BidDto> GetHigestBidAsync(Guid ArtId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Bid/Art/{ArtId}");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<BidDto>(results.Result.ToString());

            }
            return new BidDto();
        }

        public async Task<List<BidDto>> GetMyHighestBidsAsync(String UserId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Bid/MyHighestBids/{UserId}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<BidDto>>(results.Result.ToString());
            }
            return new List<BidDto>();
        }

        public async Task<List<BidDto>> GetMyWins(string UserId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Bid/MyWins/{UserId}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<BidDto>>(results.Result.ToString());
            }
            return new List<BidDto>();
        }

        public Task<ResponseDto> UpdateBid(Guid id, BidRequestDto bidRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
