using ShopOnline.Models.Dtos;
using ShopOnline.Web.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.Web.Services
{
    public class PaymentService : IPaymentService

    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreatePayment(PaymentRequestDto paymentRequestDto)
        {
            // Realiza una solicitud POST al backend que contiene el controlador Payments
            var response = await _httpClient.PostAsJsonAsync("api/payments", paymentRequestDto);

            // Asegúrate de que la solicitud fue exitosa
            response.EnsureSuccessStatusCode();

            // Devuelve el ID de la preferencia de MercadoPago
            return await response.Content.ReadAsStringAsync();
        }
    }
}
