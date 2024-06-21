using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.Services.Permission;
using PCL.Application.Services.User;
using PCL.Domain.Entities;

namespace PCL.Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            if (userDto == null) return BadRequest();

            await _userService.CreateUserAsync(userDto);

            return Ok(new
            {
                Message = "Usuário criado com sucesso",
                User = userDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(UserDto userDto)
        {
            if (userDto == null) return BadRequest();

            await _userService.UpdateUserAsync(userDto);

            return Ok(new
            {
                Message = "Usuário atualizado com sucesso",
                User = userDto
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
