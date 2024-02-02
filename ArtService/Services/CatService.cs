using ArtService.Models.Dtos;
using ArtService.Services.IServices;
using Newtonsoft.Json;

namespace ArtService.Services
{
    public class CatService : ICategory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CatService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<CategoryDto> GetCatById(string Id)
        {
            var client = _httpClientFactory.CreateClient("Category");
            var response = await client.GetAsync(Id);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.Result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(responseDto.Result.ToString());
            }
            return new CategoryDto();
        }
    }
}
