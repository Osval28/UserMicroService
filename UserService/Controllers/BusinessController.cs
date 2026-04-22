using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTOs;
using UserService.Application.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly BusinessService _businessService;

        public BusinessController(BusinessService businessService)
        {
            _businessService = businessService;
        }

        [HttpPost("create/{adminUserId}")]
        public IActionResult CreateBusiness(int adminUserId, BusinessDTO dto)
        {
            _businessService.CreateBusiness(dto, adminUserId);
            return Created("", new { message = "Business created successfully" });
        }
    }
}