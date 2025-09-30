



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
        public async Task PrintAllUserIds()
        {
            var users = await _userRepository.RetrieveAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Id}");
            }
        }
    }
}
