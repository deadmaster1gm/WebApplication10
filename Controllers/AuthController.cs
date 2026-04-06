using Microsoft.AspNetCore.Mvc;
using WebApplication10.Contracts.DTO.Authorization;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequestDto dto, CancellationToken ct)
        {
            await _service.RegisterAsync(dto, ct);
            return Ok("Регистрация успешна");
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUserRequestDto dto, CancellationToken ct)
        {
            var success = await _service.LoginAsync(dto, ct);

            if (!success)
                return Unauthorized("Неверный email или пароль");

            return Ok("Логин успешен");
        }
    }
}
