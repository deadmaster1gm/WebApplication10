using WebApplication10.Contracts.DTO;
using WebApplication10.Contracts.DTO.Authentication;

namespace WebApplication10.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterUserRequestDto dto, CancellationToken ct);
        Task<AuthResponseDto?> LoginAsync(LoginUserRequestDto dto, CancellationToken ct);
    }
}
