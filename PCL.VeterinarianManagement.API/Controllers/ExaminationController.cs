using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Examination;

namespace PCL.VeterinarianManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IExaminationService _examinationService;

        public ExaminationController(IExaminationService examinationService)
        {
            _examinationService = examinationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExaminationById(Guid id)
        {
            try
            {
                var examination = await _examinationService.GetExaminationByIdAsync(id);
                if (examination == null)
                {
                    return NotFound();
                }
                return Ok(examination);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("pet/{petId}")]
        public async Task<IActionResult> GetExaminationsByPetId(Guid petId)
        {
            try
            {
                var examinations = await _examinationService.GetExaminationsByPetIdAsync(petId);
                return Ok(examinations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> PrescribeExamination([FromBody] ExaminationDto examinationDto)
        {
            try
            {
                await _examinationService.PrescribeExaminationAsync(examinationDto);
                return CreatedAtAction(nameof(GetExaminationById), new { id = examinationDto.Id }, examinationDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExamination([FromBody] ExaminationDto examinationDto)
        {
            try
            {
                await _examinationService.UpdateExaminationAsync(examinationDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("result")]
        public async Task<IActionResult> UpdateExaminationResult([FromBody] ExaminationResultDto examinationResultDto)
        {
            try
            {
                await _examinationService.UpdateExaminationResultAsync(examinationResultDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
