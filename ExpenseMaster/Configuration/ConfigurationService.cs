﻿using ExpenseMaster.BusinessLogic.Implementations;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.BusinessLogic.Repository;
using ExpenseMaster.Common.Mapper;
using ExpenseMaster.Middlewares;
using ExpenseMaster.Model.DatabaseContext;
using ExpenseMaster.Model.Models;

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
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<IUserRegistrationService, UserRegistrationService>();
            services.AddTransient<IIncomeService, IncomeService>();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void Configure(WebApplication app)
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
