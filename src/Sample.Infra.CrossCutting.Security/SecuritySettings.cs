namespace Sample.Infra.CrossCutting.Security
{
    public class SecuritySettings
    {
        public string SecurityKey { get; set; } = default!;
        public int TimeToLive { get; set; } = 20;
        public string Issuer { get; set; } = "Issuer";
        public string Audience { get; set; } = "https://localhost";
    }
}
