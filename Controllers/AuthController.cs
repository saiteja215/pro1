using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
        {
            var user = new UserDto
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role
            };
            var result = await _authService.RegisterAsync(user, dto.Password);
            if (result == null) return BadRequest("Email already exists");
            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login(AuthDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        [HttpPost("admin-token")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> GetAdminToken(AuthDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null) return Unauthorized();
            if (result.User.Role != "Admin") return Forbid();
            return Ok(result);
        }
    }
}
