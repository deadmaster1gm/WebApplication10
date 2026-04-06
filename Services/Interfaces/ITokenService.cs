using WebApplication10.Contracts.DTO;
using WebApplication10.Entities;

namespace WebApplication10.Services.Interfaces
{
    public interface ITokenService
    {
        AuthResponseDto CreateToken(User user);
    }
}
