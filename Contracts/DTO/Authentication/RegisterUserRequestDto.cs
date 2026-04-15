namespace WebApplication10.Contracts.DTO.Authentication
{
    public record RegisterUserRequestDto(
        string Name,
        string Surname,
        string Email,
        string Password);
}
