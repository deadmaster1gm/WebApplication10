using WebApplication10.Contracts.DTO;
using WebApplication10.Contracts.DTO.Authentication;

namespace WebApplication10.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequestUserDto dto, CancellationToken ct);
        Task<AuthResponseDto?> LoginAsync(LoginRequestUserDto dto, CancellationToken ct);
    }
}
