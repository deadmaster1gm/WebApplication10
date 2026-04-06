namespace WebApplication10.Contracts.DTO
{
    public record AuthResponseDto(
        string AccessToken,
        DateTime ExpiresAt);
}
