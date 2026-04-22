using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            var token = _authService.Login(dto);
            return Ok(new { token });
        }
    }
}