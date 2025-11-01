
namespace FinanceDashboard.Application.DTOs.Category.Result
{
    public class CreateCategoryResult
    {
        public Guid? Guid { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string? Error { get; set; } = string.Empty;
    }
}
