using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Permission;
using PCL.Application.Services.Role;
using PCL.Domain.Entities;

namespace PCL.Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (roleDto == null) return BadRequest();

            await _roleService.CreateRoleAsync(roleDto);

            return Ok(new
            {
                Message = "Perfil criado com sucesso",
                Role = roleDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(RoleDto roleDto)
        {
            if (roleDto == null) return BadRequest();

            await _roleService.UpdateRoleAsync(roleDto);

            return Ok(new
            {
                Message = "Perfil atualizado com sucesso",
                Role = roleDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            await _roleService.DeleteRoleAsync(id);
            return NoContent();
        }
    }
}
