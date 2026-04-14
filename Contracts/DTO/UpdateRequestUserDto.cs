namespace WebApplication10.Contracts.DTO
{
    public class UpdateRequestUserDto
    {
        public string OrderNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
