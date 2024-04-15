using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopStyle.Core.Interfaces;
using TopStyle.Domain.DTO;
using TopStyle.Domain.Entities;

namespace TopStyle.Core.Services
{
    public class UserService : IUserService
    {
        
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserService> _logger;


        public UserService(IConfiguration configuration, UserManager<User> userManager, ILogger<UserService> logger)
        {
            
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto userDto)
        {
            _logger.LogInformation($"Attempting to register user: {userDto.UserName}");

            var userExists = await _userManager.FindByNameAsync(userDto.UserName);
            if (userExists != null)
            {
                _logger.LogError($"Registration failed. User with username '{userDto.UserName}' already exists.");
                return false;
            }

            _logger.LogInformation($"Received user registration data: UserName={userDto.UserName}, Email={userDto.Email}, Password={(userDto.Password != null ? "<not shown>" : "null")}");

            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("User registration failed for {UserName}. Errors: {Errors}", userDto.UserName, string.Join(", ", result.Errors.Select(e => e.Description)));
                return false;
            }

            _logger.LogInformation("User {UserName} registered successfully.", userDto.UserName);
            return true;
        }

        public async Task<TokenDto> AuthenticateAsync(string username, string password)
        {
            _logger.LogInformation("User trying to log in: {username}", username);

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                _logger.LogInformation("Credentials for {username} validated successfully.", username);
                return new TokenDto
                {
                    Token = CreateToken(user),
                    ExpiryDate = DateTime.UtcNow.AddDays(7)
                };
            }
            else
            {
                _logger.LogWarning("Login failed for {username}.", username);
                return null;
            }
        }


        private string CreateToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];

            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new InvalidOperationException("JWT key is not set in the configuration.");
            }
            if (string.IsNullOrEmpty(jwtIssuer))
            {
                throw new InvalidOperationException("JWT issuer is not set in the configuration.");
            }
            if (string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("JWT audience is not set in the configuration.");
            }

            var key = Encoding.UTF8.GetBytes(jwtKey);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName)
    };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = jwtIssuer,
                Audience = jwtAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}