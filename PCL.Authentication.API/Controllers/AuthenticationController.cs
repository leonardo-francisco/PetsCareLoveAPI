using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PCL.Application.Dto;
using PCL.Application.InputModels;
using PCL.Application.Services.User;
using PCL.Authentication.API.Configuration;
using PCL.Domain.Entities;
using PCL.Domain.Utils;
using System.ComponentModel.DataAnnotations;

namespace PCL.Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PasswordHasher _passwordHasher;

        public AuthenticationController(IUserService userService, PasswordHasher passwordHasher)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginInputModel model)
        {
            var userDetails = await _userService.GetUserByEmailAsync(model.Email);

            if (userDetails == null)
            {
                return NotFound("Usuario não registrado no sistema");
            }                 
            else if(userDetails != null && !_passwordHasher.VerifyPassword(userDetails.Password, model.Password))
            {
                return NotFound("Usuario ou senha incorreta");
            }

            // Gera o Token
            var token = TokenConfiguration.GenerateToken(userDetails.Name);
            // Oculta a senha
            userDetails.Password = "";
            return Ok(new
            {
                user = userDetails,
                token = token
            });
            
        }

        [HttpPost]
        [Route("registrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var user = new UserDto
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = _passwordHasher.HashPassword(userDto.Password),
                Role = userDto.Role
            };
            await _userService.CreateUserAsync(user);

            return Ok(new
            {
                Message = "Usuário criado com sucesso",
                User = user
            });
        }

        [HttpPost]
        [Route("recovery-password")]
        [AllowAnonymous]
        public async Task<IActionResult> RecoveryPassword([FromBody] LoginInputModel model)
        {
            var user = await _userService.GetUserByEmailAsync(model.Email);

            if (user == null) return NotFound("Usuario não registrado no sistema");
                      
            await _userService.UpdatePasswordAsync(user.Id, model.Password);

            return Ok("Senha alterada com sucesso");
        }
    }
}
