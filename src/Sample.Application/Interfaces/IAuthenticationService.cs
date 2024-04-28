using Sample.Infra.CrossCutting.Security.Models;

namespace Sample.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthUserResponse> Authenticate(AuthUserRequest request);
    }
}
