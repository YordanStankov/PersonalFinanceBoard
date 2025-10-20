using FinanceDashboard.Application.DTOs.Category;
using FinanceDashboard.Application.DTOs.Transaction;
using FinanceDashboard.Application.DTOs.User;
using FinanceDashboard.Application.Interfaces;
using FinanceDashboard.Domain.Interfaces;
using System.Security.Claims;


namespace FinanceDashboard.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTGeneratorService _jwtGeneratorService;
        public UserService(IUserRepository userRepository, IJWTGeneratorService jWTGeneratorService)
        {
            _userRepository = userRepository;
            _jwtGeneratorService = jWTGeneratorService;

        }

        public async Task<UserDTO> GetUser(string userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
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

        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            UserProfileDTO userProfile = new UserProfileDTO();
            var user = await _userRepository.GetUserAsync(userId);
            try
            {
                userProfile.UserName = user.UserName;
                userProfile.Categories = user.Categories.Select(c => new CategoryListDTO
                {
                    Guid = c.Guid, 
                    Name = c.Name,
                    TransactionListDTOs = c.Transactions.Select(t => new TransactionListDTO
                    {
                        TimeOfTransaction = t.Date,
                        Amount = t.Amount,
                        Description = t.Description,
                        CategoryName = t.Category != null ? t.Category.Name : "Uncategorized"
                    }).ToList()
                }).ToList();
            }
            catch (Exception ex)
            {
                userProfile.exception = ex.Message;
            }
            return userProfile;
        }

        public async Task<LoginResultDTO> LoginUser(LoginDTO loginDto)
        {
            LoginResultDTO result = new LoginResultDTO();
            try
            {
                var user = await _userRepository.LoginUser(loginDto.Email, loginDto.Password);
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

        public async Task<RegisterResultDTO> RegisterUser(RegisterDTO registerDto)
        {
            var result = new RegisterResultDTO();
            try
            {
                var user = await _userRepository.RegisterUser(registerDto.UserName, registerDto.Email, registerDto.Password);
                result.UserId = user.Id;
                result.UserName = user.UserName;
                result.Email = user.Email;
                result.IsSuccessful = true;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.Errors = new List<string> { ex.Message };
                return result;
            }
        }

    }
}
