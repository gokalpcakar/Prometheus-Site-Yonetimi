using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Prometheus.API.Helpers
{
    public class JwtService
    {
        private readonly JwtConfig configuration;
        public JwtService(IOptions<JwtConfig> _configuration)
        {
            // appsettings.json içerisinde bulunan secret alanıyla token konfigürasyonu yapılıyor
            configuration = _configuration.Value;
        }

        // token verisini dönüyor
        public string Generate(string id)
        {
            var key = Encoding.ASCII.GetBytes(configuration.Secret);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Now.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        // token'ın onaylanmasını sağladığımız metot
        public JwtSecurityToken Verify(string jwt)
        {
            var key = Encoding.ASCII.GetBytes(configuration.Secret);    
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
            }, out SecurityToken validatedToken);

            return (JwtSecurityToken) validatedToken;
        }
    }
}
