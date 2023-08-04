using Microservices.Catalog.Dtos.ProductDto;
using Microservices.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> ProductsList()
        {
            var values = await _productService.GetProductListAsync();
            return Ok(values);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetProducts(string id)
        {
            var value = await _productService.GetProductByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts(CreateProductDto createProductsDto)
        {
            await _productService.CreateProductAsync(createProductsDto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProducts(UpdateProductDto updateProductsDto)
        {
            await _productService.UpdateProductAsync(updateProductsDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
    }
}
