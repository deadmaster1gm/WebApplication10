using WebApplication10.Contracts.DTO;

namespace WebApplication10.Services.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllAsync(GetUsersQueryDto dto, CancellationToken ct);
        public Task<bool> UpdateAsync (Guid id, UpdateRequestUserDto dto, CancellationToken ct);
        public Task<bool> DeleteAsync(Guid id, CancellationToken ct);

    }
}
