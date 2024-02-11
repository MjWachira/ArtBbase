using ArtBbaseFrontend.Models.Dtos.Coupon;
using ArtBbaseFrontend.Models.Dtos;
using Newtonsoft.Json;
using System.Text;

namespace ArtBbaseFrontend.Services.Coupon
{
    public class CouponService:ICoupon
    {
        private readonly HttpClient _httpClient;
        private readonly string BASEURL = "https://artcouponservice.azurewebsites.net";
        //private readonly string BASEURL = "https://localhost:7076";
        public CouponService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ResponseDto> AddCoupon(CouponRequestDto couponRequestDto)
        {
            var request = JsonConvert.SerializeObject(couponRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api

            var response = await _httpClient.PostAsync($"{BASEURL}/api/Coupon", bodyContent);
            var content = await response.Content.ReadAsStringAsync();

            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return results;
            }
            return new ResponseDto();
        }

        public async Task<ResponseDto> DeleteCoupon(Guid id)
        {

            var response = await _httpClient.DeleteAsync($"{BASEURL}/api/Coupon/{id}");
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

        public async Task<CouponDto> GetCouponAsync(string code)
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Coupon/{code}");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<CouponDto>(results.Result.ToString());

            }
            return new CouponDto();
        }

        public async Task<List<CouponDto>> GetCouponsAsync()
        {
            var response = await _httpClient.GetAsync($"{BASEURL}/api/Coupon");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<List<CouponDto>>(results.Result.ToString());

            }
            return new List<CouponDto>();
        }

        public async Task<ResponseDto> UpdateCoupon(Guid id, CouponRequestDto couponRequestDto)
        {
            var request = JsonConvert.SerializeObject(couponRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{BASEURL}/api/Coupon/{id}", bodyContent);
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
