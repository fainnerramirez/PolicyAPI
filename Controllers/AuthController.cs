using Microsoft.AspNetCore.Mvc;
using Policies.Services;

namespace Policies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            bool isAuthenticated = _userService.AuthenticateUser(username, password);

            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            string token = _userService.GenerateToken(username);
            return Ok(new { token });
        }
    }
}