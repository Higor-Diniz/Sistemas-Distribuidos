using Microsoft.AspNetCore.Mvc;
using BlogAPI.DTOs.Auth;
using BlogAPI.Services.Interfaces;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(registrationDto);
            
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { Token = result.Data, Message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(loginDto);
            
            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(new { Token = result.Data, Message = result.Message });
        }
    }
} 