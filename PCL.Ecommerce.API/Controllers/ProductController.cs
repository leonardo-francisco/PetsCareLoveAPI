using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Category;
using PCL.Application.Services.Product;

namespace PCL.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("Category/{categoryId}")]
        public async Task<IActionResult> GetProductByCategoryId(Guid categoryId)
        {
            var product = await _productService.GetProductByCategoryIdAsync(categoryId);
            return Ok(product);
        }

        [HttpGet("CategoryAndPet/{categoryId}/{petId}")]
        public async Task<IActionResult> GetProductByCategoryAndPetId(Guid categoryId, Guid petId)
        {
            var products = await _productService.GetProductByCategoryAndPetIdAsync(categoryId, petId);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if (productDto == null) return BadRequest();

            await _productService.CreateProductAsync(productDto);

            return Ok(new
            {
                Message = "Produto criado com sucesso",
                Product = productDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            if (productDto == null) return BadRequest();

            await _productService.UpdateProductAsync(productDto);

            return Ok(new
            {
                Message = "Produto atualizado com sucesso",
                Product = productDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
