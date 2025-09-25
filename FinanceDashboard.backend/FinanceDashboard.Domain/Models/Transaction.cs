
namespace FinanceDashboard.Domain.Models
{
    public class Transaction
    {
        public Guid Guid { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; } = null;
        public Guid CategoryGuid { get; set; }
        public Category? Category { get; set; } = null;
        public string? UserId { get; set; } = null;
        public User? User { get; set; } = null; 
        public string? UserName { get; set; } = null;
    }
}
