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
                if (user.Transactions.Count > 0)
                {
                    userProfile.AverageDailySpendingPastWeek =
                        await DailySpendingCalculator(userId);

                    userProfile.MonthlySpendingAverage =
                        await MonthlySpendingCalculator(userId);
                }

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
            DateTime current = DateTime.Now;
            decimal averageMonthly = 0;

            List<DateTime> oldest = await _transactiatorsRepository
                .GetDateOfOldestAsync(userId);

            List<decimal> transactions = await _transactiatorsRepository
                .GetAllAmountsAsync(userId);

            int yearsOnApp = current.Year - oldest[0].Year;
            int monthsOnApp = 0;
            if (oldest != null)
            {
                if (yearsOnApp == 0)
                    monthsOnApp = current.Month - oldest[0].Month;
                else 
                    monthsOnApp = (yearsOnApp) * 12 + oldest[0].Month;
            }
            if (monthsOnApp != 0)
                averageMonthly = transactions.Sum() / monthsOnApp;
            else
                averageMonthly = transactions.Sum();
                return Math.Round(averageMonthly, 2);
            
        }
        private async Task<decimal> DailySpendingCalculator(string userId)
        {
            DateTime current = DateTime.Now;
            decimal averageDaysInMonth = 30.85m;
            decimal spending = 0;
            List<DateTime> oldest = await _transactiatorsRepository
                .GetDateOfOldestAsync(userId);

            List<decimal> transactions = await _transactiatorsRepository
                .GetAllAmountsAsync(userId);

            int yearsOnApp = current.Year - oldest[0].Year;
            int monthsOnApp = 0;
            decimal sum = transactions.Sum();

            if (oldest != null)
            {
                if (yearsOnApp == 0)
                    monthsOnApp = current.Month - oldest[0].Month;
                else
                    monthsOnApp = (yearsOnApp) * 12 + current.Month;
            }

            if (monthsOnApp != 0)
                spending = sum / (monthsOnApp * averageDaysInMonth);
            else
                spending = sum / (current.Day - oldest[0].Day);

                return Math.Round(spending, 2);
        }
    }
}
