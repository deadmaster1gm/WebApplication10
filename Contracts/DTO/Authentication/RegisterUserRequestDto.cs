namespace WebApplication10.Contracts.DTO.Authorization
{
    public record RegisterUserRequestDto(
        string Name,
        string Surname,
        string Email,
        string Password);
}
