

using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Interfaces;
using ShopOnline.Models.Dtos;
using ShopOnline.Api.Extensions;


using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ShopOnline.Api.Entities;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpGet("GetItems/{userId}")]
        
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId) {


            try
            {
                var cartItems = await _shoppingCartRepository.GetItems(userId);

                if (cartItems == null)
                {

                    return NoContent();

                }

                var products = await _productRepository.GetItems();

                if(products == null || !products.Any())
                {
                    throw new Exception("No products exits in the system");
                }

                var cartItemDtos = cartItems.ConvertToDto(products);

                return Ok(cartItemDtos);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }


        }

        [HttpGet("GetItem/{id:int}")]
        public async Task<ActionResult<CartItemDto>> GetItem(int id)
        {

            try
            {
                var cartItem = await _shoppingCartRepository.GetItem(id);

                if (cartItem == null)
                {
                    return NotFound();
                }

                var product= await _productRepository.GetItem(cartItem.ProductId);

                var cartItemDto= cartItem.ConvertToDto(product);

                return Ok(cartItemDto);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
           
        }
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {

            try
            {
               var cartItem= await _shoppingCartRepository.AddItem(cartItemToAddDto);

                if(cartItem == null)
                {
                    return BadRequest("Unable to add item to Cart");
                }


                var product =await  _productRepository.GetItem(cartItem.ProductId);
               
                if(product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product(productId:{cartItemToAddDto.ProductId})");
                }

                var cartItemDto = cartItem.ConvertToDto(product);

                //CreatedAtAction: Este método devuelve una respuesta HTTP con el código de estado
                //201 Created, que es el código de respuesta adecuado cuando se
                //crea un nuevo recurso en el servidor.

                //nameof(GetItem): Utiliza el nombre del método GetItem para indicar
                //cuál es la acción que puede utilizarse para recuperar el recurso
                //recién creado. nameof es una característica de C# que devuelve el
                //nombre del miembro en forma de cadena, asegurando que el nombre del
                //método sea referenciado de forma segura y sin errores tipográficos.
                //HTTP y proporcionar una respuesta adecuada al cliente después de
                //crear un nuevo recurso (en este caso, un nuevo elemento en el
                //carrito de compras).

                //new { id = cartItem.Id } : Este es un objeto anónimo que se utiliza
                //para proporcionar los valores de los parámetros de la ruta del
                //método GetItem.En este caso, GetItem probablemente tiene un parámetro
                //id en su firma, que se utiliza para obtener un elemento en particular.
                //Aquí, estamos diciendo que el id del recurso recién creado es cartItem.Id

                //cartItemDto: Este es el cuerpo de la respuesta que contiene los datos del
                //recurso recién creado.En este caso, es un objeto de tipo CartItemDto que
                //contiene los detalles del elemento del carrito de compras que se acaba de
                //agregar.


                return CreatedAtAction(nameof(GetItem), new { id = cartItem.Id }, cartItemDto);
            }
            catch (Exception ex)

            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding item to cart: {ex.Message}");
            }


        }

        [HttpDelete("DeleteItem/{id:int}")]

        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
               var deleteCartItem=await _shoppingCartRepository.DeleteItem(id);

                if (deleteCartItem == null)
                {
                    return NotFound("Cart item not found.");
                }

                var product= await _productRepository.GetItem(deleteCartItem.ProductId);

                var cartItemDto=deleteCartItem.ConvertToDto(product);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting item: {ex.Message}");
            }

        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int id, [FromBody] CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            try
            {
                var updatedCartItem=await _shoppingCartRepository.UpdateQty(id, cartItemQtyUpdateDto);

                if(updatedCartItem == null)
                {
                    return NotFound("CartItem not found");
                }

                var product= await _productRepository.GetItem(updatedCartItem.ProductId);
               
                if(product == null)
                {
                    return NotFound("Associated product not found.");
                }

                var cartItemDto = updatedCartItem.ConvertToDto(product);

                return Ok(cartItemDto);

            }
            catch (Exception ex)
            {

               return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating quantity: {ex.Message}");
            }



        }

    }

} 
		
	
	




