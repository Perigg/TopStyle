using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TopStyle.Core.Services;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly TokenService _tokenService;

        public UserController(UserManager<User> userManager, ILogger<UserController> logger, TokenService tokenService)
        {
            _logger = logger;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
            {
                _logger.LogError("Received a null DTO");
                return BadRequest("Invalid user data");
            }

            _logger.LogInformation("Registering user with username: {Username}, email: {Email}", userRegisterDto.UserName, userRegisterDto.Email);

            var user = new User
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email
               
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User {UserName} registered successfully.", userRegisterDto.UserName);
                return Ok("User registered successfully");
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Failed to register user: {Username}, Errors: {Errors}", userRegisterDto.UserName, errors);
                return BadRequest($"Failed to register user: {errors}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await _tokenService.CreateTokenAsync(user);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }


    }
}

