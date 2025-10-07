
using FinanceDashboard.Application.DTOs.Category;

namespace FinanceDashboard.Application.DTOs.Transaction
{
    public class CreateTransactionDTO
    {
        public Decimal Amount { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
        public ICollection<CategoryForTransactionCreationDTO> Categories { get; set; } = new List<CategoryForTransactionCreationDTO>();
        public Guid CategoryGuid { get; set; }
        public string? Description { get; set; } = null;
        public string? UserId { get; set; } = null;

    }
}
