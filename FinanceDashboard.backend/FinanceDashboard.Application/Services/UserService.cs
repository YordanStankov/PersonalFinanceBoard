using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.DTOs.User.Result;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Domain.Models;
using Microsoft.AspNetCore.Identity;


namespace FinanceDashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IJWTGeneratorService _jwtGeneratorService;
        private readonly ITransactionsRepository _transactiatorsRepository;
        public UserService(IUserRepository userRepository, 
            IJWTGeneratorService jWTGeneratorService, 
            ITransactionsRepository transactiatorsRepository,
            UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _jwtGeneratorService = jWTGeneratorService;
            _transactiatorsRepository = transactiatorsRepository;
            _userManager = userManager;
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
                    userProfile.AverageDailySpending =
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
                var user = await _userRepository
                .GetByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    result.IsSuccessful = false;
                    result.Exception = "No user with this email adress!";
                }
                else if(await _userManager.CheckPasswordAsync(user, loginDto.Password) == false)
                {
                    result.IsSuccessful = false;
                    result.Exception = "Wrong password!";
                }
                else 
                {
                    result.IsSuccessful = true;
                    result.Token = _jwtGeneratorService.GenerateToken(user.Id, user.Email, user.UserName);
                    result.Expiration = DateTime.UtcNow.AddHours(1);
                    result.UserId = user.Id;
                    result.Email = user.Email;
                    result.UserName = user.UserName;
                }

            return result;
        }

        public async Task<RegisterResultDTO> RegisterAsync(RegisterDTO registerDto)
        {
            var result = new RegisterResultDTO();
            
            if( await _userRepository.CheckForExistingUserNameAsync(registerDto.UserName))
            {
                result.IsSuccessful = false;
                result.Error = $"Error: UserName: {registerDto.UserName} is already taken.";
            }
            else if (await _userRepository.CheckForExistingEmailAsync(registerDto.Email))
            {
                result.IsSuccessful = false;
                result.Error = $"Error: Email: {registerDto.Email} is already registered.";
            }
            else
            {
                    User newUser = new User
                    {
                        UserName = registerDto.UserName,
                        Email = registerDto.Email
                    };
                var output = await _userManager.CreateAsync(newUser, registerDto.Password)
                .ContinueWith(async task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        await _userRepository.SaveChangesAsync();
                        return newUser.Id;
                    }
                    else
                    {
                        result.IsSuccessful = false;
                        result.Error = "Error: User registration failed.";
                        return null;
                    }
                });
                    if(output.Result != null)
                {
                    result.UserId = output.Result;
                    result.Token = _jwtGeneratorService.GenerateToken(output.Result, newUser.Email, newUser.UserName);
                    result.UserName = newUser.UserName;
                    result.Email = newUser.Email;
                    result.IsSuccessful = true;
                }
            }
            return result;
        }

        private async Task<decimal> MonthlySpendingCalculator(string userId)
        {
            DateTime current = DateTime.Now;
            
            List<DateTime> oldest = await _transactiatorsRepository
                .GetDateOfOldestAsync(userId);

            List<decimal> transactions = await _transactiatorsRepository
                .GetAllAmountsAsync(userId);

            int yearsOnApp = current.Year - oldest[0].Year;
            int monthsOnApp = yearsOnApp > 0 
                ? (yearsOnApp) * 12 + (12 - oldest[0].Month) 
                : current.Month - oldest[0].Month;

            decimal averageMonthly = monthsOnApp == 0 
                ? transactions.Sum() 
                : transactions.Sum() / monthsOnApp;

            return Math.Round(averageMonthly, 2);
        }

        private async Task<decimal> DailySpendingCalculator(string userId)
        {
            DateTime current = DateTime.Now;
            const decimal averageDaysInMonth = 30.85m;

            List<DateTime> oldest = await _transactiatorsRepository
                .GetDateOfOldestAsync(userId);

            List<decimal> transactions = await _transactiatorsRepository
                .GetAllAmountsAsync(userId);

            int yearsOnApp = current.Year - oldest[0].Year;
            decimal sum = transactions.Sum();

            int monthsOnApp = yearsOnApp > 0
                ? (yearsOnApp) * 12 + (12 - oldest[0].Month)
                : current.Month - oldest[0].Month;

            decimal spending = monthsOnApp != 0 
                ? sum / (monthsOnApp * averageDaysInMonth)
                : sum / (current.Day - oldest[0].Day);

            return Math.Round(spending, 2);
        }
    }
}
