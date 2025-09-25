
using Microsoft.AspNetCore.Identity;

namespace FinanceDashboard.Domain.Models
{
    public class User : IdentityUser
    {
       public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
       public ICollection<Category> Categories { get; set; } = new List<Category>();
       
    }
}
