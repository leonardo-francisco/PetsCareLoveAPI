using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Permission;

namespace PCL.Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(permissions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPermissionById(Guid id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            return Ok(permission);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(PermissionDto permDto)
        {
            if (permDto == null) return BadRequest();

            await _permissionService.CreatePermissionAsync(permDto);

            return Ok(new
            {
                Message = "Permissão criada com sucesso",
                Permission = permDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(PermissionDto permDto)
        {
            if (permDto == null) return BadRequest();

            await _permissionService.UpdatePermissionAsync(permDto);

            return Ok(new
            {
                Message = "Permissão atualizada com sucesso",
                Permission = permDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            await _permissionService.DeletePermissionAsync(id);
            return NoContent();
        }
    }
}
