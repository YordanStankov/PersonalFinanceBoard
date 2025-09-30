
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace FinanceDashboard.Infrastructure.Seeding
{
    public static class CategorySeeding
    {
        public static async Task<Task> SeedCategories(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context.Categories.Any())
            {
                var AllCategories = await context.Categories.ToListAsync();

                return Task.CompletedTask; // DB has been seeded
            }
            var user = await context.Users.FirstOrDefaultAsync();
            var categories = new List<Domain.Models.Category>
            {
                new Domain.Models.Category { Name = "Groceries" , UserId = user.Id},
                new Domain.Models.Category { Name = "Utilities" , UserId = user.Id},
                new Domain.Models.Category { Name = "Entertainment" , UserId = user.Id},
                new Domain.Models.Category { Name = "Transportation" , UserId = user.Id},
                new Domain.Models.Category { Name = "Healthcare" , UserId = user.Id},
                new Domain.Models.Category { Name = "Dining Out" , UserId = user.Id},
                new Domain.Models.Category { Name = "Travel" , UserId = user.Id},
                new Domain.Models.Category { Name = "Education" , UserId = user.Id},
                new Domain.Models.Category { Name = "Personal Care" , UserId = user.Id},
                new Domain.Models.Category { Name = "Miscellaneous" , UserId = user.Id}
            };
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
