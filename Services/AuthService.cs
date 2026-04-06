using Microsoft.AspNetCore.Identity;
using WebApplication10.Contracts.DTO.Authorization;
using WebApplication10.Entities;
using WebApplication10.Exceptions;
using WebApplication10.Repositories.Interfaces;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }
        public async Task RegisterAsync(RegisterUserRequestDto dto, CancellationToken ct)
        {
            _logger.LogInformation("Попытка регистрации пользователя");

            var existingUser = await _userRepository.GetByEmailAsync(dto.Email, ct);

            if (existingUser is not null)
            {
                throw new EmailAlreadyExistsException(dto.Email);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                TimeCreated = DateTime.UtcNow,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _userRepository.AddAsync(user, ct);

            _logger.LogInformation($"Пользователь с {dto.Email} зарегистрирован");
        }
        public async Task<bool> LoginAsync(LoginUserRequestDto dto, CancellationToken ct)
        {
            _logger.LogInformation("Попытка логина");

            var user = await _userRepository.GetByEmailAsync(dto.Email, ct);

            if (user is null)
                return false;

            var result = _passwordHasher.VerifyHashedPassword(
                            user,
                            user.PasswordHash,
                            dto.Password);

            _logger.LogInformation("Логин успешен");

            return result != PasswordVerificationResult.Failed;
        }
    }
}
