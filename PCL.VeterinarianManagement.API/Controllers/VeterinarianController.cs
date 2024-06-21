using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Veterinarian;

namespace PCL.VeterinarianManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeterinarianController : ControllerBase
    {
        private readonly IVeterinarianService _veterinarianService;

        public VeterinarianController(IVeterinarianService veterinarianService)
        {
            _veterinarianService = veterinarianService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVeterinarians()
        {
            var vets = await _veterinarianService.GetAllVeterinarians();
            return Ok(vets);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VeterinarianDto>> GetVeterinarianByIdAsync(Guid id)
        {
            var veterinarian = await _veterinarianService.GetVeterinarianByIdAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            return Ok(veterinarian);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVeterinarian(VeterinarianDto vetDto)
        {
            if (vetDto == null) return BadRequest();

            await _veterinarianService.CreateVeterinarianAsync(vetDto);

            return Ok(new
            {
                Message = "Veterinario criado com sucesso",
                Veterinarian = vetDto
            });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateVeterinarianAsync(Guid id, [FromBody] VeterinarianDto vetDto)
        {
            if (id != vetDto.Id)
            {
                return BadRequest("Veterinarian ID mismatch");
            }

            await _veterinarianService.UpdateVeterinarianAsync(vetDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinarian(Guid id)
        {
            await _veterinarianService.DeleteVeterinarianAsync(id);
            return NoContent();
        }

        [HttpGet("{id:guid}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByVeterinarianIdAsync(Guid id)
        {
            var appointments = await _veterinarianService.GetAppointmentsByVeterinarianIdAsync(id);
            return Ok(appointments);
        }

        [HttpGet("{id:guid}/pets")]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetPetsByVeterinarianIdAsync(Guid id)
        {
            var pets = await _veterinarianService.GetPetsByVeterinarianIdAsync(id);
            return Ok(pets);
        }

        [HttpPost("examinations")]
        public async Task<IActionResult> PrescribeExaminationAsync([FromBody] ExaminationDto examination)
        {
            await _veterinarianService.PrescribeExaminationAsync(examination);
            return CreatedAtAction(nameof(PrescribeExaminationAsync), new { id = examination.Id }, examination);
        }

        [HttpGet("{id:guid}/examination-results")]
        public async Task<ActionResult<IEnumerable<ExaminationResultDto>>> GetExaminationResultsByVeterinarianIdAsync(Guid id)
        {
            var examinationResults = await _veterinarianService.GetExaminationResultsByVeterinarianIdAsync(id);
            return Ok(examinationResults);
        }

        [HttpGet("medical-records/{petId:guid}")]
        public async Task<ActionResult<MedicalRecordDto>> GetMedicalRecordByPetIdAsync(Guid petId)
        {
            var medicalRecord = await _veterinarianService.GetMedicalRecordByPetIdAsync(petId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }
    }
}
