



using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Domain.Interfaces;

namespace FinanceDashboard.Application.Services
{
    public class UserService : Interfaces.IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> GetUser(string userId)
        {
            var user = await _userRepository.GetUser(userId);
            return new UserDTO
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Transactions = user.Transactions.Select(t => new DTOs.Transaction.TransactionDTO
                {
                    Guid = t.Guid,
                    Amount = t.Amount,
                    Date = t.Date,
                    Description = t.Description,
                    CategoryGuid = t.CategoryGuid,
                    UserId = t.UserId
                }).ToList(),
                Categories = user.Categories.Select(c => new DTOs.Category.CategoryDTO
                {
                    Guid = c.Guid,
                    Name = c.Name,
                    UserId = c.UserId
                }).ToList()
            };
        }
    }
}
