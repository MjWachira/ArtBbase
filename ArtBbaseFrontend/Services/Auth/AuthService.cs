using ArtBbaseFrontend.Models.Dtos;
using ArtBbaseFrontend.Models.Dtos.Auth;
using Newtonsoft.Json;
using System.Text;

namespace ArtBbaseFrontend.Services.Auth
{
    public class AuthService : IAuth
    {
        private readonly HttpClient _httpClient;
        //private readonly string BASEURL = "https://thejitucommerceauthapi.azurewebsites.net";
        private readonly string BASEURL = "https://localhost:7269";
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var request = JsonConvert.SerializeObject(loginRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/login", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<LoginResponseDto>(results.Result.ToString());

            }
            return new LoginResponseDto();
        }

        public async Task<ResponseDto> Register(RegisterRequestDto registerRequestDto)
        {
            var request = JsonConvert.SerializeObject(registerRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/User/register", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;

            }
            return new ResponseDto();
        }
    }
}
