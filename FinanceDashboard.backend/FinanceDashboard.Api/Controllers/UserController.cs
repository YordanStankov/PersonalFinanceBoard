using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("UserController is working!");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var result = await _userService.RegisterAsync(registerDto);
            if (result.IsSuccessful == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var result = await _userService.LoginAsync(loginDTO);
            if(result.IsSuccessful == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("LoadProfile")]
        public async Task<IActionResult> LoadProfile([FromBody] object userId)
        {
            var UserProfile = await _userService.GetProfileAsync(userId.ToString() ?? "userId");
            if (UserProfile.IsSuccess == true)
                return Ok(UserProfile);
            else 
                return BadRequest(UserProfile);
        }
    }
}
