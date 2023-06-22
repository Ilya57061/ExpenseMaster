using ExpenseMaster.Middlewares;
using ExpenseMaster.DAL.DatabaseContext;
using ExpenseMaster.DAL.Seed;
using ExpenseMaster.BusinessLogic.Infrastructure.Mapper;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.DAL.Interfaces;
using ExpenseMaster.DAL.Repository;
using ExpenseMaster.DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<DataSeeder>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IEmailService,EmailService>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IIncomeService, IncomeService>();
            services.AddTransient<IExpenseService, ExpenseService>();
            services.AddTransient<IBudgetService, BudgetService>();
            services.AddTransient<IFinancialGoalService, FinancialGoalService>();


            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddAutoMapper(typeof(RoleMappingProfile).Assembly);

            services.AddCustomServices();
            services.AddCustomAutoMapper();
            services.AddEndpointsApiExplorer();
            services.AddCustomSwagger();
            services.AddCustomLogging();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
    .GetBytes(configuration.GetSection("Authentication:Secret").Value)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["Authentication:Issuer"],
        ValidAudience = configuration["Authentication:Audience"]
    };
});
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
