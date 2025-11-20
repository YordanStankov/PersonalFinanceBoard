
using FinanceDashboard.Application.DTOs.Category;

namespace FinanceDashboard.Application.DTOs.User
{
    public class UserProfileDTO
    {
        public string? UserName { get; set; } = string.Empty;
        public Decimal? MonthlyIncome { get; set; } = 0;
        public Decimal? MonthlySpendingAverage { get; set; } = 0;   
        public Decimal? AverageDailySpending { get; set; } = 0;
        public ICollection<CategoryListDTO>? Categories { get; set; } = new List<CategoryListDTO>();
        public string? Error { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }   
    }
}
