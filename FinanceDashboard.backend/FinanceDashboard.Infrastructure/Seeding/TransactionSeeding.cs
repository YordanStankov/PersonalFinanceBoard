using FinanceDashboard.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceDashboard.Infrastructure.Seeding
{
    public static class TransactionSeeding
    {
        public static async Task<Task> SeedTransactions(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Transactions.Any())
            {
                var AllTransactions = await context.Transactions.ToListAsync();
                foreach (var transaction in AllTransactions)
                {
                    Console.WriteLine($"Transaction ID: {transaction.Guid}, Amount: {transaction.Amount}, Date: {transaction.Date}, Description: {transaction.Description}, CategoryId: {transaction.CategoryGuid}, UserId: {transaction.UserId}");
                }
                return Task.CompletedTask; 
            }
            else
            {
                var users = await context.Users.ToListAsync();
                var categories = await context.Categories.ToListAsync();

                List<Transaction> transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Amount = 50.75m,
                        Date = DateTime.UtcNow.AddDays(-10),
                        Description = "Grocery shopping at SuperMart",
                        Category = categories[1],
                        CategoryGuid = categories[1].Guid,
                        UserId = users[1].Id,
                        User = users[1]
                    },

                 new Transaction
                {
                    Amount = 520.75m,
                    Date = DateTime.UtcNow.AddDays(-10),
                    Description = "Description description description",
                    Category = categories[3],
                    CategoryGuid = categories[3].Guid,
                    UserId = users[0].Id,
                    User = users[0]
                },
                 new Transaction
                {
                    Amount = 613.00m,
                    Date = DateTime.UtcNow.AddDays(-10),
                    Description = "Description description description",
                    Category = categories[2],
                    CategoryGuid = categories[2].Guid,
                    UserId = users[2].Id,
                    User = users[2]
                },
                 new Transaction
                {
                    Amount = 2.50m,
                    Date = DateTime.UtcNow.AddDays(-10),
                    Description = "Description description description",
                    Category = categories[5],
                    CategoryGuid = categories[5].Guid,
                    UserId = users[3].Id,
                    User = users[3]
                }

            };


                await context.Transactions.AddRangeAsync(transactions);
                await context.SaveChangesAsync();
            }
            return Task.CompletedTask;
        }
    }
}
