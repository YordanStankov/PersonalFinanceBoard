
using FinanceDashboard.Application.DTOs.Transaction;

namespace FinanceDashboard.Application.DTOs.Category
{
    public class CategoryListDTO
    {
        public Guid Guid { get; set; }
        public string Name {  get; set; } = string.Empty;
        public List<TransactionListDTO> TransactionListDTOs { get; set; } = new List<TransactionListDTO>();
    }
}
