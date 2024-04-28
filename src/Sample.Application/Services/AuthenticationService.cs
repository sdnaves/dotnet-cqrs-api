using Sample.Application.Interfaces;
using Sample.Infra.CrossCutting.Security.Interfaces;
using Sample.Infra.CrossCutting.Security.Models;
using Sample.Infra.CrossCutting.Security.Utilities;
using System.Security;

namespace Sample.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtBuilder _jwtBuilder;

        private readonly IAccountService _accountService;

        public AuthenticationService(IJwtBuilder jwtBuilder, IAccountService accountService)
        {
            _jwtBuilder = jwtBuilder;

            _accountService = accountService;
        }

        public async Task<AuthUserResponse> Authenticate(AuthUserRequest request)
        {
            var account = await _accountService.GetByEmail(request.Email);

            if (account is null)
                throw new SecurityException("Invalid credentials");

            if (!account.PasswordHash.Equals(SecurityUtility.ComputeSha256Hash(request.Password)))
                throw new SecurityException("Invalid credentials");

            var accessToken = _jwtBuilder.BuildToken(account.Email, account.Id);

            return new AuthUserResponse
            {
                Email = account.Email,
                Name = account.Name,
                Token = new AuthUserResponse.AuthUserTokenResponse
                {
                    ExpiresIn = _jwtBuilder.TimeToLive * 60,
                    AccessToken = accessToken,
                    RefreshToken = SecurityUtility.ComputeSha256Hash(account.Id)
                }
            };
        }
    }
}
