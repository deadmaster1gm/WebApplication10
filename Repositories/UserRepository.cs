using Microsoft.EntityFrameworkCore;
using WebApplication10.Contracts.DTO;
using WebApplication10.Data;
using WebApplication10.Entities;
using WebApplication10.Infrastructure.Sorting;
using WebApplication10.Repositories.Interfaces;

namespace WebApplication10.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly ISortMap<User> _sortMap;

        public UserRepository(AppDbContext context, ISortMap<User> sort)
        {
            _context = context;
            _sortMap = sort;
        }
        public async Task<IEnumerable<User>> GetAllAsync(GetUsersQueryDto query, CancellationToken ct)
        {
            var users = _context.Users.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                users = users.Where(u =>
                        u.Name.ToLower().Contains(query.Search) ||
                        u.Surname.ToLower().Contains(query.Search) ||
                        u.Email.ToLower().Contains(query.Search) ||
                        u.OrderNumber.ToLower().Contains(query.Search));
            }

            bool desc = string.Equals(query.SortDir, "desc", StringComparison.OrdinalIgnoreCase);

            if (!string.IsNullOrWhiteSpace(query.SortBy) &&
                _sortMap.Map.TryGetValue(query.SortBy, out var keySelector))
            {
                users = desc
                    ? users.OrderByDescending(keySelector)
                    : users.OrderBy(keySelector);
            }
            else
            {
                users = users.OrderBy(u => u.Name);
            }

            return await users.ToListAsync(ct);
        }
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
        public async Task AddAsync(User user, CancellationToken ct)
        {
            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task RemoveAsync(User user, CancellationToken ct)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(ct);
        }
        public async Task UpdateAsync(User user, CancellationToken ct)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> ExistsByEmailAsync(string email, Guid id, CancellationToken ct)
        {
            return await _context.Users.AnyAsync(u => u.Email == email && u.Id != id);
        }
    }
}
