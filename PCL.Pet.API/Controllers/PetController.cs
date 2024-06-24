using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Pet;

namespace PCL.Pet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPets()
        {
            var pets = await _petService.GetAllPetsAsync();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(Guid id)
        {
            var pet = await _petService.GetPetByIdAsync(id);
            return Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet(PetDto petDto)
        {
            if (petDto == null) return BadRequest();

            await _petService.CreatePetAsync(petDto);

            return Ok(new
            {
                Message = "Animal criado com sucesso",
                Pet = petDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(PetDto petDto)
        {
            if (petDto == null) return BadRequest();

            await _petService.UpdatePetAsync(petDto);

            return Ok(new
            {
                Message = "Animal atualizado com sucesso",
                Pet = petDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(Guid id)
        {         
            await _petService.DeletePetAsync(id);
            return NoContent();
        }
    }
}
