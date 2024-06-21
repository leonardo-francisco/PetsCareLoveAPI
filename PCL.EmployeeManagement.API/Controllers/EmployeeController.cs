using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Employee;

namespace PCL.EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync(Guid employeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployeeAsync([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null) return BadRequest();

            await _employeeService.CreateEmployeeAsync(employeeDto);

            return Ok(new
            {
                Message = "Funcionário criado com sucesso",
                Employee = employeeDto
            });
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployeeAsync([FromBody] EmployeeDto employeeDto)
        {
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeDto.Id);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _employeeService.UpdateEmployeeAsync(employeeDto);
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<ActionResult> DeleteEmployeeAsync(Guid employeeId)
        {
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            await _employeeService.DeleteEmployeeAsync(employeeId);
            return NoContent();
        }

        [HttpPost("service")]
        public async Task<ActionResult<ServiceDto>> CreateServiceAsync([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null) return BadRequest();

            var createdService = await _employeeService.CreateServiceAsync(serviceDto);

            return Ok(new
            {
                Message = "Serviço criado com sucesso",
                Service = serviceDto
            });
        }

        [HttpGet("attend/{serviceType}")]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> AttendByServiceTypeAsync(int serviceType)
        {
            var employees = await _employeeService.AttendByServiceTypeAsync(serviceType);
            return Ok(employees);
        }
    }
}
