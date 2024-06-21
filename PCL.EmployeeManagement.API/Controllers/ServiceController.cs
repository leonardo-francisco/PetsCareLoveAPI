using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Service;

namespace PCL.EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServicesAsync()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetServiceByIdAsync(Guid id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult> CreateServiceAsync([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null) return BadRequest();

            await _serviceService.CreateServiceAsync(serviceDto);

            return Ok(new
            {
                Message = "Serviço criado com sucesso",
                Serviço = serviceDto
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateServiceAsync([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null) return BadRequest();

            var existingService = await _serviceService.GetServiceByIdAsync(serviceDto.Id);

            if (existingService == null)
            {
                return NotFound();
            }

            await _serviceService.UpdateServiceAsync(serviceDto);

            return Ok(new
            {
                Message = "Serviço atualizado com sucesso",
                Serviço = serviceDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteServiceAsync(Guid id)
        {
            var existingService = await _serviceService.GetServiceByIdAsync(id);

            if (existingService == null)
            {
                return NotFound();
            }

            await _serviceService.DeleteServiceAsync(id);
            return NoContent();
        }
    }
}
