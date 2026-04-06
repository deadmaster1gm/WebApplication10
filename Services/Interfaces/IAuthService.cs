using WebApplication10.Contracts.DTO.Authorization;

namespace WebApplication10.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync (RegisterUserRequestDto dto, CancellationToken ct);
        Task <bool> LoginAsync (LoginUserRequestDto dto, CancellationToken ct);
    }
}
