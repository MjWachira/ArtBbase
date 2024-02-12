using ArtBbaseFrontend.Models.Dtos;
using ArtBbaseFrontend.Models.Dtos.Art;
using Newtonsoft.Json;
using System.Text;

namespace ArtBbaseFrontend.Services.Art
{
    public class ArtService : IArt
    {
        private readonly HttpClient _httpClient;
        // private readonly string BASEURL = "https://localhost:7053";
        private readonly string BASEURL = "https://artservice20240208085307.azurewebsites.net";
        public ArtService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDto> AddArt(ArtRequestDto product)
        {
            var request = JsonConvert.SerializeObject(product);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Art", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of Arts
                return results;

            }

            return new ResponseDto();
        }

        public async Task<ResponseDto> DeleteArt(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Art/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of Arts
                return results;

            }

            return new ResponseDto();
        }

        public async Task<ArtDto> GetArtByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of Arts
                return JsonConvert.DeserializeObject<ArtDto>(results.Result.ToString());

            }
            return new ArtDto();
        }

        public async Task<List<ArtDto>> GetArtsAsync()
        {

            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of arts
                return JsonConvert.DeserializeObject<List<ArtDto>>(results.Result.ToString());

            }
            return new List<ArtDto>();
        }

        public async Task<List<ArtDto>> GetClosedArtsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art/ClosedArts");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of arts
                return JsonConvert.DeserializeObject<List<ArtDto>>(results.Result.ToString());

            }
            return new List<ArtDto>();
        }

        public async Task<List<ArtDto>> GetOpenArtsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art/OpenArt");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of arts
                return JsonConvert.DeserializeObject<List<ArtDto>>(results.Result.ToString());

            }
            return new List<ArtDto>();
        }

        public async Task<List<ArtDto>> GetUserArtsAsync(Guid UserId)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Art/userArt/{UserId}");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of arts
                return JsonConvert.DeserializeObject<List<ArtDto>>(results.Result.ToString());

            }
            return new List<ArtDto>();
        }

        public async Task<ResponseDto> UpdateArt(Guid id, ArtRequestDto productRequestDto)
        {
            var request = JsonConvert.SerializeObject(productRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Art/{id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (results.IsSuccess)
            {
                //change this to a list of art
                return results;

            }

            return new ResponseDto();
        }
        public async Task<ResponseDto> UpdateHighestBid(Guid artId, double newHighestBid)
        {
            try
            {
                var encodedArtId = Uri.EscapeDataString(artId.ToString());
                var encodedBid = Uri.EscapeDataString(newHighestBid.ToString());

                var response = await _httpClient.PostAsync($"{BASEURL}/api/Art/UpdateHighestBid?artId={encodedArtId}&newHighestBid={encodedBid}", null);
                var content = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<ResponseDto>(content);
                return results;
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                Console.WriteLine($"Error updating highest bid: {ex.Message}");
                return new ResponseDto { IsSuccess = false, Errormessage = "An error occurred while updating highest bid" };
            }
        }

    }
}
