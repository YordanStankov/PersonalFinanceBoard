
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.User;

namespace FinanceDashboard.Application.DTOs.Transaction
{
    public class TransactionDTO
    {
        public Guid Guid { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = null;
        public Guid CategoryGuid { get; set; }
        public CategoryDTO? Category { get; set; } = null;
        public string? UserId { get; set; } = null;
        public UserDTO? User { get; set; } = null;
        public string? UserName { get; set; } = null;
    }
}
