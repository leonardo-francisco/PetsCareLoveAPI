using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Appointment;

namespace PCL.VeterinarianManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("veterinarian/{veterinarianId}")]
        public async Task<IActionResult> GetAppointmentsByVeterinarianId(Guid veterinarianId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByVeterinarianIdAsync(veterinarianId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetAppointmentsByOwnerId(Guid ownerId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByOwnerIdAsync(ownerId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ScheduleAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                await _appointmentService.ScheduleAppointmentAsync(appointmentDto);
                return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentDto.Id }, appointmentDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            try
            {
                await _appointmentService.UpdateAppointmentAsync(appointmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(Guid id)
        {
            try
            {
                await _appointmentService.CancelAppointmentAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
