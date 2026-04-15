using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using spydle_api.Interfaces;
using spydle_api.Models;

namespace spydle_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid request" });
            }

            var user = await _userManager.FindByEmailAsync(registerRequest.Email);
            if (user == null)
            {
                User registeringUser = new User
                {
                    Email = registerRequest.Email,
                    UserName = registerRequest.Username
                };
                var result = await _userManager.CreateAsync(registeringUser, registerRequest.Password);
                if (result.Succeeded)
                {
                    var token = _tokenService.GenerateToken(registeringUser);
                    return Ok(token.Result);
                }
                return StatusCode(500, new { errors = result.Errors });
            }
            else
            {
                return BadRequest(new { error = "A user with the same email address is already registered." });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid request" });
            }

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            var signInResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, true);
            if (signInResult.Succeeded)
            {
                var token = await _tokenService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest("Incorrect email or password.");
        }
    }
}
