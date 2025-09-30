using FinanceDashboard.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceDashboard.Infrastructure.Seeding
{
    public static class UserSeeding
    {
        public static Task SeedUsers(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var serviceProvider = scope.ServiceProvider.GetRequiredService<IServiceProvider>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            if (context.Users.Any())
            {
                var AllUsers = context.Users.ToList();
                foreach (var user in AllUsers)
                {
                    Console.WriteLine($"User ID: {user.Id}");
                }
                return Task.CompletedTask; // DB has been seeded
            }
            var users = new List<User>
            {
                new User { UserName = "user1", Email = "user1@example.com" },
                new User { UserName = "user2", Email = "user2@example.com" },
                new User { UserName = "user3", Email = "testuser1@example.com" },
                new User { UserName = "admin", Email = "testuser2@gmail.com" }  
            };

            foreach (var user in users)
            {
                if (userManager.FindByNameAsync(user.UserName).Result == null)
                {
                    userManager.CreateAsync(user, "Password123!").Wait();
                }
            }

            return Task.CompletedTask;
        }
    }
}
