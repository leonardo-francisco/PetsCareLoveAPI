using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Breed;

namespace PCL.Pet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IBreedService _breedService;

        public BreedController(IBreedService breedService)
        {
            _breedService = breedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBreeds()
        {
            var breeds = await _breedService.GetAllBreedsAsync();
            return Ok(breeds);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBreedById(Guid id)
        {
            var breed = await _breedService.GetBreedByIdAsync(id);
            return Ok(breed);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBreed(BreedDto breedDto)
        {
            if (breedDto == null) return BadRequest();

            await _breedService.CreateBreedAsync(breedDto);

            return Ok(new
            {
                Message = "Raça criada com sucesso",
                Project = breedDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBreed(BreedDto breedDto)
        {
            if (breedDto == null) return BadRequest();

            await _breedService.UpdateBreedAsync(breedDto);

            return Ok(new
            {
                Message = "Raça atualizada com sucesso",
                Project = breedDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreed(Guid id)
        {
            await _breedService.DeleteBreedAsync(id);
            return NoContent();
        }
    }
}
