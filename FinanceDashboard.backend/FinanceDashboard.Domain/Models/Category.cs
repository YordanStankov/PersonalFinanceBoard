
using System.ComponentModel.DataAnnotations;

namespace FinanceDashboard.Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Guid { get; set; }
        public string? Name { get; set; } = null;
        public string UserId { get; set; } = null!; 
        public User? User { get; set; } = null;     
    }
}
