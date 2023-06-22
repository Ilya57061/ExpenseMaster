using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Repository;
using ExpenseMaster.Middlewares;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Models;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Seed;
using Microsoft.Extensions.DependencyInjection;
using System;
using ExpenseMaster.BusinessLogic.Infrastructure.Mapper;


namespace ExpenseMaster.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDatabase(configuration);
            services.AddControllers();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryBase<User>, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IIncomeService, IncomeService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddScoped<DataSeeder>();

            services.AddTransient<IBudgetService, BudgetService>();
            services.AddTransient<IFinancialGoalService, FinancialGoalService>();


            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddAutoMapper(typeof(RoleMappingProfile).Assembly);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void Configure(WebApplication app, IServiceProvider serviceProvider)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            ConfigureDataSeeder(serviceProvider);

            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
        public static void ConfigureDataSeeder(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDatabaseContext>();
                var dataSeeder = new DataSeeder(context);
                dataSeeder.Initialize();
            }
        }
    }
}
