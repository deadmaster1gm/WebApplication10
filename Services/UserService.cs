using WebApplication10.Contracts.DTO;
using WebApplication10.Entities;
using WebApplication10.Exceptions;
using WebApplication10.Repositories.Interfaces;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository repository, ILogger<UserService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync(GetUsersQueryDto dto, CancellationToken ct)
        {
            _logger.LogInformation("Попытка получения списка пользователей");

            var users = await _repository.GetAllAsync(dto, ct);

            _logger.LogInformation("Пользователи получены");

            return users.Select(MapToDto)
                        .ToList();
        }
        public async Task<bool> UpdateAsync(Guid id, UpdateRequestUserDto dto, CancellationToken ct)
        {
            _logger.LogInformation("Попытка обновления данных пользователя");

            var user = await _repository.GetByIdAsync(id, ct);

            if (user is null)
                return false;

            var normalizedEmail = dto.Email.Trim().ToLowerInvariant();

            var exists = await _repository.ExistsByEmailAsync(normalizedEmail, id, ct);

            if(exists)
            {
                throw new EmailAlreadyExistsException(dto.Email);
            }

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.OrderNumber = dto.OrderNumber;

            await _repository.UpdateAsync(user, ct);

            _logger.LogInformation("Пользователь обновлен");

            return true;
        }
        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
        {
            _logger.LogInformation("Попытка удаления пользователя");

            var user = await _repository.GetByIdAsync(id, ct);

            if (user is null)
                return false;

            await _repository.RemoveAsync(user, ct);

            _logger.LogInformation("Пользователь удален");

            return true;
        }
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                TimeCreated = user.TimeCreated,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                OrderNumber = user.OrderNumber,
            };
        }
    }
}
