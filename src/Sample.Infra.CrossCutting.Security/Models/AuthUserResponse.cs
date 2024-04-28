namespace Sample.Infra.CrossCutting.Security.Models
{
    public class AuthUserResponse
    {
        public string Email { get; set; } = default!;
        public string Name { get; set; } = default!;
        public required AuthUserTokenResponse Token { get; set; }

        public class AuthUserTokenResponse : AuthTokenResponse
        {
            public required string RefreshToken { get; set; }
            public string? SessionToken { get; set; }
        }
    }
}
