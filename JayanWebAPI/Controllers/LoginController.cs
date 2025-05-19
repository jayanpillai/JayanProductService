using DataModel;
using DataService.ConcreteClasses;
using DataService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JayanWebAPI.Static;

namespace JayanWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserService userService;
        private readonly IConfiguration _configuration;
        public LoginController(ILogger<LoginController> logger,IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            this.userService = userService;
        }

        [HttpPost(Name = "Login")]
        public IActionResult LoginForUser(string userName)
        {
            UserDataModel doesUserExists = userService.get(userName);

            if (doesUserExists == null)
                return NotFound($"InValid Account:{userName}");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,_configuration[UtilityForAPI.JWT_SUBJECT]),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Upn,doesUserExists.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[UtilityForAPI.JWT_KEY]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                _configuration[UtilityForAPI.JWT_ISSUER],
                _configuration[UtilityForAPI.JWT_AUDIENCE],
                claims,
                expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>(UtilityForAPI.JWT_TOKENTIMEOUT)),
                signingCredentials: signIn
                );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation($"New Token Created for User:{doesUserExists.UserName}");
            return Ok(new { Token = tokenValue, User = doesUserExists.UserName });

        }

    }
}
