using ExpenseMaster.BusinessLogic.Infrastructure.Mapper;

namespace ExpenseMaster.Configuration
{
    public static class AutoMapperExtensions
    {
        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BudgetMappingProfile).Assembly);
            services.AddAutoMapper(typeof(FinancialGoalMappingProfile).Assembly);
            services.AddAutoMapper(typeof(UserMappingProfile).Assembly);
            services.AddAutoMapper(typeof(RoleMappingProfile).Assembly);
        }
    }
}
