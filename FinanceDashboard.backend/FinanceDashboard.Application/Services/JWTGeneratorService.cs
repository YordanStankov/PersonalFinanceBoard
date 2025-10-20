
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;

namespace FinanceDashboard.Application.Services
{
    public class JWTGeneratorService : IJWTGeneratorService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationMinutes;
        private readonly IUserRepository _userRepository;

        public JWTGeneratorService(IConfiguration configuration, IUserRepository userRepository)
        {
            _secret = configuration["Jwt:Secret"];
            _issuer = configuration["Jwt:Issuer"];
            _audience = configuration["Jwt:Audience"] ?? "FinanceDashboardUsers";
            _expirationMinutes = int.TryParse(configuration["Jwt:ExpirationInMinutes"], out var expiration) ? expiration : 60;
            _userRepository = userRepository;
        }


        public string GenerateToken(string userId, string email, string username)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                Claims = new Dictionary<string, object>()
                {
                    {"Id", userId },
                    {"Email", email },
                    {"Name", username }
                },
                Issuer = _issuer,
                
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(descriptor); 
            return token;
        }
    }
}
