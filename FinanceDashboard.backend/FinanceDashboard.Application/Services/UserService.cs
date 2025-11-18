using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.DTOs.User.Result;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;


namespace FinanceDashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTGeneratorService _jwtGeneratorService;
        private readonly ITransactionsRepository _transactiatorsRepository;
        public UserService(IUserRepository userRepository, 
            IJWTGeneratorService jWTGeneratorService, 
            ITransactionsRepository transactiatorsRepository)
        {
            _userRepository = userRepository;
            _jwtGeneratorService = jWTGeneratorService;
            _transactiatorsRepository = transactiatorsRepository;
        }

        public async Task<UserDTO> GetAsync(string userId)
        {
            var user = await _userRepository.GetAsync(userId);
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

        public async Task<UserProfileDTO> GetProfileAsync(string userId)
        {
            UserProfileDTO userProfile = new UserProfileDTO();
            var user = await _userRepository.GetAsync(userId);
            if (user == null || user.Email == null)
            {
                userProfile.IsSuccess = false;
                userProfile.Error = "User not found.";
            }
            else
            {
                userProfile.IsSuccess = true;
                userProfile.UserName = user.UserName;
                userProfile.AverageDailySpendingPastWeek = 
                    await DailySpendingCalculator(userId);

                userProfile.MonthlySpendingAverage = 
                    await MonthlySpendingCalculator(userId);

                userProfile.Categories = user.Categories.Select(c => new CategoryListDTO
                {
                    Guid = c.Guid,
                    Name = c.Name,
                    TransactionListDTOs = c.Transactions.Select(t => new TransactionListDTO
                    {
                        Guid = t.Guid,
                        TimeOfTransaction = t.Date,
                        Amount = t.Amount,
                        Description = t.Description,
                        CategoryName = t.Category != null ? t.Category.Name : "Uncategorized"
                    }).ToList()
                }).ToList();
            }
            return userProfile;
        }

        public async Task<LoginResultDTO> LoginAsync(LoginDTO loginDto)
        {
            LoginResultDTO result = new LoginResultDTO();
            try
            {
                var user = await _userRepository.LoginAsync(loginDto.Email, loginDto.Password);
                if (user != null)
                {
                    result.IsSuccessful = true;
                    result.Token = _jwtGeneratorService.GenerateToken(user.Id, user.Email, user.UserName);
                    result.Expiration = DateTime.UtcNow.AddHours(1);
                    result.UserId = user.Id;
                    result.Email = user.Email;
                    result.UserName = user.UserName;
                }
                else
                {
                    result.IsSuccessful = false;
                    result.Exception = "Wrong credentials";
                }
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Token = null;
                result.Expiration = DateTime.MinValue;
                result.Exception = ex.Message;    
            }
            return result;
        }

        public async Task<RegisterResultDTO> RegisterAsync(RegisterDTO registerDto)
        {
            var result = new RegisterResultDTO();
            
                string output = await _userRepository.RegisterAsync(registerDto.UserName, registerDto.Email, registerDto.Password);
            if (!output.StartsWith("Error"))
            {
                var user = await _userRepository.GetAsync(output);
                result.UserId = user.Id;
                result.Token = _jwtGeneratorService.GenerateToken(user.Id, user.Email, user.UserName);
                result.UserName = user.UserName;
                result.Email = user.Email;
                result.IsSuccessful = true;
            }

            else if (output.StartsWith("Error:"))
            {
                result.IsSuccessful = false;
                result.Error = output;
            }
            return result;
        }

        private async Task<decimal> MonthlySpendingCalculator(string userId)
        {
            List<int> oldest = await _transactiatorsRepository
                .GetMonthOfOldestAsync(userId);

            List<decimal> transactions = await _transactiatorsRepository
                .GetAllAmountsAsync(userId, oldest[0]);

            int differential = DateTime.Now.Month - oldest[0];
            decimal monthlyAverage = 0;

            if (oldest[0] > 0 && differential != 0)
                    monthlyAverage = transactions.Sum() / differential;
            else
                 monthlyAverage = transactions.Sum();

            return Math.Round(monthlyAverage, 2);
            
        }
        private async Task<decimal> DailySpendingCalculator(string userId)
        {
            List<int> oldest = await _transactiatorsRepository
                .GetDayOfOldestAMonthBackAsync(userId);

            List<decimal> monthlyTransactions = await _transactiatorsRepository
                .GetAmountsAMonthBackAsync(userId);

            decimal differential = 30 - oldest[0];
            decimal dailyAverage = 0;

            if (oldest[0] == 0 || differential <= 0)
                dailyAverage = monthlyTransactions.Sum();
            else 
                dailyAverage = monthlyTransactions.Sum() / differential;

                return Math.Round(dailyAverage);
        }
    }
}
