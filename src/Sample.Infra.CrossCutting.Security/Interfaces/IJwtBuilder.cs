namespace Sample.Infra.CrossCutting.Security.Interfaces
{
    public interface IJwtBuilder
    {
        int TimeToLive { get; }
        string BuildToken(string principalId, string securityIdentifier);
    }
}
