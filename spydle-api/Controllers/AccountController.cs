using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using spydle_api.Interfaces;
using spydle_api.Mappers;
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
                return BadRequest(new ErrorContainer(ModelState.ToErrorCollection()));
            }

            var findByEmailTask = _userManager.FindByEmailAsync(registerRequest.Email);
            var findByNameTask = _userManager.FindByNameAsync(registerRequest.Username);
            await Task.WhenAll(findByEmailTask, findByNameTask);

            var userByEmail = findByEmailTask.Result;
            if (userByEmail != null)
            {
                return BadRequest(new ErrorContainer("A user with the same email address is already registered."));
            }

            var userByName = findByNameTask.Result;
            if (userByName != null)
            {
                return BadRequest(new ErrorContainer("A user with the same username is already registered."));
            }

            User registeringUser = new User
            {
                Email = registerRequest.Email,
                UserName = registerRequest.Username
            };
            var result = await _userManager.CreateAsync(registeringUser, registerRequest.Password);
            if (result.Succeeded)
            {
                return Ok("Successfuly registered.");
            }
            return StatusCode(500, new ErrorContainer(result.Errors.Select(identityError => identityError.Description).ToArray()));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorContainer(ModelState.ToErrorCollection()));
            }

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return BadRequest(new ErrorContainer("Incorrect email or password."));
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, false, true);
            if (signInResult.Succeeded)
            {
                var token = await _tokenService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest(new ErrorContainer("Incorrect email or password."));
        }
    }
}
