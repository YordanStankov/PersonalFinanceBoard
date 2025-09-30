using FinanceDashboard.Infrastructure;
using FinanceDashboard.Infrastructure.Seeding;
using FinanceDashboard.Domain.Models; 
using Microsoft.AspNetCore.Identity;   
using Microsoft.EntityFrameworkCore;
using FinanceDashboard.Domain.Interfaces;
using FinanceDashboard.Infrastructure.Repositories;
using FinanceDashboard.Application.Services;
using FinanceDashboard.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Identity services
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//Registering Services
builder.Services.AddScoped<IUserService, UserService>();


//Registering Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
await app.SeedUsers();
await app.SeedCategories();
await app.SeedTransactions();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
