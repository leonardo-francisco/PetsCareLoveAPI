using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.MedicalRecord;

namespace PCL.VeterinarianManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;

        public MedicalRecordController(IMedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        // GET: api/MedicalRecord/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicalRecordById(Guid id)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(id);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }

        // GET: api/MedicalRecord/Pet/{petId}
        [HttpGet("Pet/{petId}")]
        public async Task<IActionResult> GetMedicalRecordByPetId(Guid petId)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordByPetIdAsync(petId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }

        // GET: api/MedicalRecord/Appointment/{appointmentId}
        [HttpGet("Appointment/{appointmentId}")]
        public async Task<IActionResult> GetMedicalRecordByAppointmentId(Guid appointmentId)
        {
            var medicalRecord = await _medicalRecordService.GetMedicalRecordByAppointmentIdAsync(appointmentId);
            if (medicalRecord == null)
            {
                return NotFound();
            }
            return Ok(medicalRecord);
        }

        // POST: api/MedicalRecord
        [HttpPost]
        public async Task<IActionResult> AddMedicalRecord([FromBody] MedicalRecordDto medicalRecord)
        {
            if (medicalRecord == null)
            {
                return BadRequest("Invalid medical record data.");
            }

            await _medicalRecordService.AddMedicalRecordAsync(medicalRecord);
            return CreatedAtAction(nameof(GetMedicalRecordById), new { id = medicalRecord.Id }, medicalRecord);
        }

        // PUT: api/MedicalRecord/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedicalRecord(Guid id, [FromBody] MedicalRecordDto medicalRecord)
        {
            if (medicalRecord == null || id != medicalRecord.Id)
            {
                return BadRequest("Invalid medical record data.");
            }

            var existingRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(id);
            if (existingRecord == null)
            {
                return NotFound();
            }

            await _medicalRecordService.UpdateMedicalRecordAsync(medicalRecord);
            return NoContent();
        }
    }
}
