using ArtBbaseFrontend.Models.Dtos.Category;
using ArtBbaseFrontend.Models.Dtos;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using ArtBbaseFrontend.Models.Dtos.Coupon;

namespace ArtBbaseFrontend.Services.Category
{
    public class CategoryService : ICategory
    {
        private readonly HttpClient _httpClient;
        //private readonly string _baseUrl = "https://localhost:7207";
        private readonly string _baseUrl = "https://artcategoryservice.azurewebsites.net";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDto> AddCategory(CategoryRequestDto categoryRequestDto)
        {
            var request = JsonConvert.SerializeObject(categoryRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");
            //communicate wih Api
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/Category", bodyContent);
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of categories
                return results;
            }
            return new ResponseDto();
        }

        public async Task<ResponseDto> DeleteCategory(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/Category/{id}");
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


        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Category");
            var content = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                return JsonConvert.DeserializeObject<List<CategoryDto>>(results.Result.ToString());
            }
            return new List<CategoryDto>();
        }

        public async Task<CategoryDto> GetCategoryAsync(Guid Id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/Category/{Id}");
            var content = await response.Content.ReadAsStringAsync();


            var results = JsonConvert.DeserializeObject<ResponseDto>(content);

            if (results.IsSuccess)
            {
                //change this to a list of products
                return JsonConvert.DeserializeObject<CategoryDto>(results.Result.ToString());

            }
            return new CategoryDto();
        }

        public async Task<ResponseDto> UpdateCategory(Guid id, CategoryRequestDto categoryRequestDto)
        {
            var request = JsonConvert.SerializeObject(categoryRequestDto);
            var bodyContent = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/api/Category/{id}", bodyContent);
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
