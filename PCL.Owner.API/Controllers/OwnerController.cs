using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Owner;

namespace PCL.Owner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owners = await _ownerService.GetAllOwnersAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(Guid id)
        {
            var owner = await _ownerService.GetOwnerByIdAsync(id);
            return Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerDto ownerDto)
        {
            if (ownerDto == null) return BadRequest();

            await _ownerService.AddOwnerAsync(ownerDto);

            return Ok(new
            {
                Message = "Tutor criado com sucesso",
                Owner = ownerDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(OwnerDto ownerDto)
        {
            if (ownerDto == null) return BadRequest();

            await _ownerService.UpdateOwnerAsync(ownerDto);

            return Ok(new
            {
                Message = "Tutor atualizado com sucesso",
                Owner = ownerDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(Guid id)
        {
            await _ownerService.DeleteOwnerAsync(id);
            return NoContent();
        }
    }
}
