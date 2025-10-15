using System.IdentityModel.Tokens.Jwt;

namespace FinanceDashboard.Application.DTOs.User
{
    public class LoginResultDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }    
        public bool IsSuccessful { get; set; }
        public string Exception { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
