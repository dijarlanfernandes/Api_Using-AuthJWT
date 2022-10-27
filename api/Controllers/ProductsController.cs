using api.Mappings.Dtos;
using api.Models;
using api.Repository.ProductRepository.Interface;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/api")]
    [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IProduct _product;
        public ProductsController(IProduct product)
        {
            _product = product;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Products()
        {
            var list = await _product.GetProductsAllAsync();
            var listproductDto = list.ConvertListProductToDtos();
            return Ok(listproductDto);
        }

        [HttpPost("products")]
        public async Task<ActionResult<ProductDto>> PostProducts(Product product)
        {
            try
            {
                var products = await _product.PostProduct(product);
                if (products == null)
                {
                    var code = HttpStatusCode.NotFound;
                    return StatusCode(((int)code),"not found");
                }                           
                    var productDto = products.ConvertProductToDto();
                    return Ok(productDto);
                
            }
            catch (Exception)
            {
                return BadRequest("erro no servidor");
                throw;
            }
           

        }
    }
}
