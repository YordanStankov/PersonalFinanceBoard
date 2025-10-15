

namespace FinanceDashboard.Application.Interfaces
{
    public interface IJWTGeneratorService
    {
        string GenerateToken(string userId, string email, string username);
    }
}
