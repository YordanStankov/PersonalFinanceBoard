


using FinanceDashboard.Application.DTOs.User;

namespace FinanceDashboard.Application.DTOs.Category
{
    public class CategoryDTO
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; } = null;
        public string UserId { get; set; } = null!;
        public UserDTO? User { get; set; } = null;
    }
}
