
namespace FinanceDashboard.Application.DTOs.User
{
    public class LoginResultDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }    
        public bool IsSuccessful { get; set; }
    }
}
