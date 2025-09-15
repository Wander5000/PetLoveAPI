using Microsoft.AspNetCore.Mvc;
using PetLoveAPI.Services.Auth;
using PetLoveAPI.DTOs.Auth;

namespace PetLoveAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Log-In")]
        public async Task<IActionResult> Login(LoginDTO request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("Sign-In")]
        public async Task<IActionResult> Register(RegisterDTO request)
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }

        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> Forgot_Password(ForgotPasswordEmailDTO request)
        {
            return Ok();
        }
    }
}
