using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Breed;
using PCL.Application.Services.Gender;

namespace PCL.Pet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GenderController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenders()
        {
            var genders = await _genderService.GetAllGendersAsync();
            return Ok(genders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenderById(Guid id)
        {
            var gender = await _genderService.GetGenderByIdAsync(id);
            return Ok(gender);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGender(GenderDto genderDto)
        {
            if (genderDto == null) return BadRequest();

            await _genderService.CreateGenderAsync(genderDto);

            return Ok(new
            {
                Message = "Gênero criado com sucesso",
                Gender = genderDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGender(GenderDto genderDto)
        {
            if (genderDto == null) return BadRequest();

            await _genderService.UpdateGenderAsync(genderDto);

            return Ok(new
            {
                Message = "Gênero atualizado com sucesso",
                Gender = genderDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGender(Guid id)
        {
            await _genderService.DeleteGenderAsync(id);
            return NoContent();
        }
    }
}
