
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

                //GetFromJsonAsync es un método de extensión que realiza una solicitud
                //GET a la URL especificada (en este caso, "api/Product") y deserializa
                //la respuesta JSON a un tipo especificado (IEnumerable<ProductDto>).
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/product/{categoryId}/GetItemsByCategory");

                // Verifica si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Si el código de estado es 204 (No Content), devuelve una lista vacía
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }
                    // Deserializa el contenido de la respuesta
                    var productCategories = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();

                    // Si la deserialización falla o el resultado es nulo, maneja ese caso
                    return productCategories ?? Enumerable.Empty<ProductDto>();
                }
                else
                {
                    // Lee el contenido del error y lanza una excepción detallada
                    var message = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error al obtener las categorías de productos. Código: {response.StatusCode}, Message - {message}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Registra y maneja errores específicos de la solicitud HTTP
                throw new HttpRequestException($"Error en la solicitud HTTP: {httpEx.Message}, httpEx");
            }
            catch (Exception ex)
            {

                // Lanza una excepción genérica y preserva el stack trace
                throw new Exception($"Error al recuperar las categorías de productos: {ex.Message}", ex);
            }
        }




        public async Task<IEnumerable<ProductCategoryDto>> GetProductCategories()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/product/productCategories");

                // Verifica si la respuesta es exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Si el código de estado es 204 (No Content), devuelve una lista vacía
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductCategoryDto>();
                    }
                    // Deserializa el contenido de la respuesta
                    var categories = await response.Content.ReadFromJsonAsync<IEnumerable<ProductCategoryDto>>();

                    // Si la deserialización falla o el resultado es nulo, maneja ese caso
                    return categories ?? Enumerable.Empty<ProductCategoryDto>();
                }
                else
                {
                    // Lee el contenido del error y lanza una excepción detallada
                    var message = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error al obtener las categorías de productos. Código: {response.StatusCode}, Message - {message}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Registra y maneja errores específicos de la solicitud HTTP
                throw new HttpRequestException($"Error en la solicitud HTTP: {httpEx.Message}, httpEx");
            }
            catch (Exception ex)
            {

                // Lanza una excepción genérica y preserva el stack trace
                throw new Exception($"Error al recuperar las categorías de productos: {ex.Message}", ex);
            }
        }


    }
}