using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTO_s;
using UserService.Application.DTOs;
using UserService.Application.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Application.Services.UserServices _userServices;

        public UserController(Application.Services.UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDTO userDTO)
        {
            var user = _userServices.CreateUser(userDTO);
            return Created("", new { message = "User created successfully", id = user.Id });
        }
    }
}