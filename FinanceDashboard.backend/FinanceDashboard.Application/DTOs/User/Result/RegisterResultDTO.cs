namespace FinanceDashboard.Application.DTOs.User.Result
{
    public class RegisterResultDTO
    {
        public string UserId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;   
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
