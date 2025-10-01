
using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;

namespace FinanceDashboard.Application.DTOs.User
{
    public class UserDTO
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<TransactionDTO> Transactions { get; set; } = new List<TransactionDTO>();
        public ICollection<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    }
}
