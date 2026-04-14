using Microsoft.AspNetCore.Identity;
using WebApplication10.Contracts.DTO;
using WebApplication10.Contracts.DTO.Authentication;
using WebApplication10.Entities;
using WebApplication10.Exceptions;
using WebApplication10.Repositories.Interfaces;
using WebApplication10.Services.Interfaces;

namespace WebApplication10.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _passwordHasher = new PasswordHasher<User>();
            _tokenService = tokenService;
        }
        public async Task RegisterAsync(RegisterUserRequestDto dto, CancellationToken ct)
        {
            var existingUser = await _repository.GetByEmailAsync(dto.Email, ct);

            if(existingUser is not null)
            {
                throw new EmailAlreadyExistsException(dto.Email);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                OrderNumber = string.Empty,
                Role = Roles.User
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _repository.AddAsync(user, ct);
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginUserRequestDto dto, CancellationToken ct)
        {
            var user = await _repository.GetByEmailAsync(dto.Email, ct);

            if (user is null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(
                        user,
                        user.PasswordHash,
                        dto.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return _tokenService.CreateToken(user);
        }
    }
}
