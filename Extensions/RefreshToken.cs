namespace ems_backend.Extensions
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}