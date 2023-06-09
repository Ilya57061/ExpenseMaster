﻿using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Models;
using ExpenseMaster.DAL.Repository;
using ExpenseMaster.DAL.Seed;

namespace ExpenseMaster.Configuration
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryBase<User>, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<DataSeeder>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IIncomeService, IncomeService>();
            services.AddTransient<IBudgetService, BudgetService>();
            services.AddTransient<IFinancialGoalService, FinancialGoalService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
