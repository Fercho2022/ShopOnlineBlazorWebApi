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
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetItems();
                var categories = await _productRepository.GetAllCategories();
                if (products == null || categories == null)
                {
                    return NotFound();


                }
                else
                {
                    //invoca al metodo de extensión de la clase static ProductExtensions
                    var productDtos = products.ConvertToProductDto(categories);

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
                var product = await _productRepository.GetItemById(id);
               
                if (product == null)
                {
                    return NotFound();


                }
                else
                {

                    var productCategory = await _productRepository.GetCategoryById(product.CategoryId);
                    //invoca al metodo de extensión de la clase static ProductExtensions
                    var productDto = product.ConvertToProductDto(productCategory);

                    // Devolver la lista de ProductDto
                    return Ok(productDto);

                }



            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta de error genérica
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");

            }

        }

    }
}
