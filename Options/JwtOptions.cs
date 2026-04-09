namespace WebApplication10.Options
{
    public class JwtOptions
    {
        public string SectionName { get; set; } = "Jwt";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int AccessTokenMinutes { get; set; }
    }
}
