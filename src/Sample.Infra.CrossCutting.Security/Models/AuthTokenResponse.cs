namespace Sample.Infra.CrossCutting.Security.Models
{
    public class AuthTokenResponse
    {
        public string TokenType { get; set; } = "bearer";
        public required int ExpiresIn { get; set; }
        public required string AccessToken { get; set; }
    }
}
