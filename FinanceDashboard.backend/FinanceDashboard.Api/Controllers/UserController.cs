using FinanceDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Api.Controllers
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("UserController is working!");
        }
    }
}
