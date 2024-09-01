using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace ShopOnline.Web.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient _httpClient;
               

        public ShoppingCartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CartItemDto> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAddDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default(CartItemDto);
                    }

                    return await response.Content.ReadFromJsonAsync<CartItemDto>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CartItemDto> DeleteItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/ShoppingCart/deleteItem/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                return default(CartItemDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<CartItemDto>> GetItems(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/ShoppingCart/GetItems/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CartItemDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} Message: {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving items from cart: {ex.Message}");
            }
        }

       
        public Task<CartItemDto> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItemDto> PostItem(CartItemToAddDto cartItemToAddDto)
        {

            try
            {
                //PostAsJsonAsync serializa automaticamente al cuerpo del objeto cartItemToAddDto como un Json
                var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAddDto);

                //Verifica si la respuesta del servidor tiene un código de estado de éxito (2xx).
                if (response.IsSuccessStatusCode)
                {

                    //Si el código de estado es NoContent (204), retornamos default(CartItemDto),
                    //lo cual devuelve null en este caso porque CartItemDto es una clase.
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        // Si el servidor devuelve NoContent, retornamos el valor por defecto
                        return default(CartItemDto);
                    }

                    // De lo contrario, deserializamos la respuesta y retornamos el CartItemDto
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }
                else
                {
                    //Si la respuesta no es de éxito, se lee el mensaje de error y se lanza
                    //una excepción con ese mensaje.
                    var message = await response.Content.ReadAsStringAsync();

                    throw new Exception($"Http status: {response.StatusCode} - Message - {message}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding item to cart: {ex.Message}");
            }


        }

        public async Task<CartItemDto> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                //PatchAsJsonAsync serializa automaticamente al cuerpo del objeto cartItemQtyUpdateDto como un Json
                var response = await _httpClient.PatchAsJsonAsync<CartItemQtyUpdateDto>($"api/ShoppingCart/{id}", cartItemQtyUpdateDto);

                // Verifica si la respuesta fue exitosa
                if (response.IsSuccessStatusCode)
                {

                    // Lee y devuelve el resultado como CartItemDto
                    return await response.Content.ReadFromJsonAsync<CartItemDto>();
                }

                else
                {
                    // Maneja el caso en que la respuesta no sea exitosa
                    // // Lee el contenido de la respuesta como una cadena de texto.
                    var message = await response.Content.ReadAsStringAsync();

                    throw new HttpRequestException($"Error updating quantity: {message}");
                }
            }
            catch (Exception ex)
            {

                // Maneja excepciones de manera adecuada
                throw new ApplicationException($"Error in {nameof(UpdateQty)}: {ex.Message}", ex);
            }
        }
    }
}