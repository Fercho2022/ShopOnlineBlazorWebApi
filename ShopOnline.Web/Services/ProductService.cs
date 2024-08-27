
using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        public async Task<ProductDto> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(ProductDto);
                    }
                    return await response.Content.ReadFromJsonAsync<ProductDto>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;

            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}

