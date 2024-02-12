using ArtBbaseFrontend.Models.Dtos;
using ArtBbaseFrontend.Models.Dtos.Bid;
using ArtBbaseFrontend.Models.Dtos.Order;
using Newtonsoft.Json;
using System.Text;

namespace ArtBbaseFrontend.Services.Order
{
    public class OrderService : IOrder
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl = "https://localhost:7035";
        private readonly string _baseUrl = "https://artorderservice.azurewebsites.net";

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<OrdeDto> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var request = JsonConvert.SerializeObject(stripeRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Order/pay", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<StripeRequestDto>(results.Result.ToString());
            }
            return new StripeRequestDto();
        }

        public async Task<ResponseDto> PlaceOrder(Guid Id, OrderRequestDto orderRequestDto)
        {
            var request = JsonConvert.SerializeObject(orderRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Order/{Id}", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of categories
                return results;
            }
            return new ResponseDto();
        }

        public async Task<List<OrdeDto>> UserOrdersAsync(string UserId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Order/User/{UserId}");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<OrdeDto>>(results.Result.ToString());
            }
            return new List<OrdeDto>();
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Order/validate/{OrderId}", null);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return true;
            }
            return false;
        }
    }
}
