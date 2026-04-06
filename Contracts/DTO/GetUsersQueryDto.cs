namespace WebApplication10.Contracts.DTO
{
    public class GetUsersQueryDto
    {
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string? SortDir {  get; set; }
    }
}
