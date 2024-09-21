using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<string>> CreatePreference([FromBody] PaymentRequestDto request)
        {


            // Asignar el AccessToken de Mercado Pago
            MercadoPagoConfig.AccessToken = "APP_USR-1933944003545279-092112-3fd30be245fe531fd27de6a708e76655-1999250409";

            // Crear la preferencia de pago
            var preferenceRequest= new PreferenceRequest
            {

                Items = new List<PreferenceItemRequest>
                        {
                            new PreferenceItemRequest
                            {
                        Title = request.Title,     // Título del producto
                        Quantity = request.Quantity,  // Cantidad
                        CurrencyId = "ARS",           // Moneda (puedes cambiarla según el país)
                        UnitPrice = request.Price / request.Quantity   // Precio unitario
                    }
                }
            };

            // Crear la preferencia usando el cliente
            var client = new PreferenceClient();
            Preference preference = await client.CreateAsync(preferenceRequest);

            // Devolver el ID de la preferencia generada
            return preference.Id != null ? Ok(preference.Id) : BadRequest("No se pudo crear la preferencia");
        }
    }
}