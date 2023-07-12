using Microsoft.AspNetCore.Mvc;
using simple_crud_and_authentication.Models;
using simple_crud_and_authentication.Services;


namespace simple_crud_and_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public UserController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // TODO: Implement user registration logic and save the user in the database

            // Return the generated JWT token upon successful registration
            var token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // TODO: Implement user login logic and verify credentials

            // Return the generated JWT token upon successful login
            var token = _authenticationService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }

}
