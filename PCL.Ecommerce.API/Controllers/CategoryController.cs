using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Breed;
using PCL.Application.Services.Category;

namespace PCL.Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest();

            await _categoryService.CreateCategoryAsync(categoryDto);

            return Ok(new
            {
                Message = "Categoria criada com sucesso",
                Category = categoryDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest();

            await _categoryService.UpdateCategoryAsync(categoryDto);

            return Ok(new
            {
                Message = "Categoria atualizada com sucesso",
                Category = categoryDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
