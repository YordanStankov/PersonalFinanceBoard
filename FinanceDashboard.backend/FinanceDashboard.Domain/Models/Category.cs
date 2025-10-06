
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceDashboard.Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Guid { get; set; }
        public string? Name { get; set; } = null;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!; 
        public User? User { get; set; } = null;
        [InverseProperty(nameof(Transaction.Category))]
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
