
using FinanceDashboard.Application.DTOs.Transaction;

namespace FinanceDashboard.Application.DTOs.Category
{
    public class CategoryListDTO
    {
        public string Name {  get; set; } = string.Empty;
        public List<TransactionListDTO> TransactionListDTOs { get; set; } = new List<TransactionListDTO>();
    }
}
