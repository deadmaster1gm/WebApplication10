using Microsoft.AspNetCore.Mvc;
using WebApplication10.Contracts.DTO.Authentication;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto dto, CancellationToken ct)
        {
            await _authService.RegisterAsync(dto, ct);

            return Ok("Регистрация успешна");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginUserRequestDto dto, CancellationToken ct)
        {
            var response = await _authService.LoginAsync(dto, ct);

            if (response is null)
                return Unauthorized("Неверный email или пароль");

            return Ok(response);
        }
    }
}
