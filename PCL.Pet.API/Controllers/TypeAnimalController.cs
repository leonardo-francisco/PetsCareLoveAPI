using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Gender;
using PCL.Application.Services.TypeAnimal;

namespace PCL.Pet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeAnimalController : ControllerBase
    {
        private readonly ITypeAnimalService _typeAnimalService;

        public TypeAnimalController(ITypeAnimalService typeAnimalService)
        {
            _typeAnimalService = typeAnimalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await _typeAnimalService.GetAllTypeAnimalsAsync();
            return Ok(types);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTypeById(Guid id)
        {
            var type = await _typeAnimalService.GetTypeAnimalByIdAsync(id);
            return Ok(type);
        }

        [HttpPost]
        public async Task<IActionResult> CreateType(TypeAnimalDto typeAnimalDto)
        {
            if (typeAnimalDto == null) return BadRequest();

            await _typeAnimalService.CreateTypeAnimalAsync(typeAnimalDto);

            return Ok(new
            {
                Message = "Tipo de animal criado com sucesso",
                Tipo = typeAnimalDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateType(TypeAnimalDto typeAnimalDto)
        {
            if (typeAnimalDto == null) return BadRequest();

            await _typeAnimalService.UpdateTypeAnimalAsync(typeAnimalDto);

            return Ok(new
            {
                Message = "Tipo de animal atualizado com sucesso",
                Tipo = typeAnimalDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            await _typeAnimalService.DeleteTypeAnimalAsync(id);
            return NoContent();
        }
    }
}
