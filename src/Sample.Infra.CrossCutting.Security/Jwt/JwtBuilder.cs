using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sample.Infra.CrossCutting.Security.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.Infra.CrossCutting.Security.Jwt
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly SecuritySettings _settings;

        public int TimeToLive => _settings.TimeToLive;

        public JwtBuilder(IOptions<SecuritySettings> settings)
        {
            ArgumentNullException.ThrowIfNull(settings);

            _settings = settings.Value;
        }

        public string BuildToken(string principalId, string securityIdentifier)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecurityKey));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var roleClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, principalId),
                new Claim(JwtRegisteredClaimNames.Sid, securityIdentifier)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = new ClaimsIdentity(roleClaims),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_settings.TimeToLive),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
