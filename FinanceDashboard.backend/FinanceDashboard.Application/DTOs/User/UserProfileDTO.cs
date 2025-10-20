
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;

namespace FinanceDashboard.Application.DTOs.User
{
    public class UserProfileDTO
    {
        public string UserName { get; set; } = string.Empty;
        public Decimal MonthlyIncome { get; set; } = 0;
        public Decimal MonthlySpending { get; set; } = 0;   
        public Decimal AverageDailySpending { get; set; } = 0;
        public ICollection<CategoryListDTO> Categories { get; set; } = new List<CategoryListDTO>();
        public string exception { get; set; } = string.Empty;
    }
}
