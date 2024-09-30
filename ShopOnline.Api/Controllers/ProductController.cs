using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Api.Extensions;
using ShopOnline.Api.Interfaces;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await _productRepository.GetItems();
               
                if (products == null )
                {
                    return NotFound();


                }
                else
                {
                    //invoca al metodo de extensión de la clase static ProductExtensions
                    var productDtos = products.ConvertToDto();

                    // Devolver la lista de ProductDto
                   
                    return Ok(productDtos);

                }

            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error genérica
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await _productRepository.GetItem(id);
               
                if (product == null)
                {
                    return NotFound();


                }
               

                   
                    //invoca al metodo de extensión de la clase static ProductExtensions
                    var productDto = product.ConvertToDto();

                    // Devolver la lista de ProductDto
                    return Ok(productDto);

            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error genérica
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }

        }

        [HttpGet("productCategories")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await _productRepository.GetCategories();

                if(productCategories == null)
                {
                    return NotFound();
                }
                else
                {
                    var productCategoryDto = productCategories.ConvertToDto();
                   
                    return Ok(productCategoryDto);
                }
            }
            catch (Exception ex)
            {

                // Manejar la excepción y devolver una respuesta de error genérica
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{categoryId}/GetItemsByCategory")]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                // Obtener productos con las categorías incluidas en una sola consulta
                var products = await _productRepository.GetItemsByCategory(categoryId);

              
                var productDtos= products.ConvertToDto();

                return Ok(productDtos);

            }
            catch (Exception ex)
            {

                // Manejar la excepción y devolver una respuesta de error genérica
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }



    }
}
