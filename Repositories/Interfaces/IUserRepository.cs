using WebApplication10.Contracts.DTO;
using WebApplication10.Contracts.DTO.Authorization;
using WebApplication10.Entities;

namespace WebApplication10.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllAsync(GetUsersQueryDto dto, CancellationToken ct);
        public Task<User?> GetByIdAsync (Guid id, CancellationToken ct);
        public Task<User?> GetByEmailAsync(string email, CancellationToken ct);
        public Task AddAsync(User user, CancellationToken ct);
        public Task UpdateAsync(User user, CancellationToken ct);
        public Task RemoveAsync(User user, CancellationToken ct);
        public Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
        public Task<bool> ExistsByEmailAsync(string email, Guid id, CancellationToken ct);
    }
}
