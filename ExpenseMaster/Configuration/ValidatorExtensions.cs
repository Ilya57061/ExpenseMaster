using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Validators;
using ExpenseMaster.BusinessLogic.Validators.Interfaces;
using ExpenseMaster.BusinessLogic.Validators.Wrapper;
using FluentValidation;

namespace ExpenseMaster.Configuration
{
    public static class ValidatorExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<IncomeItemDto>, IncomeItemDtoValidator>();
            services.AddTransient<IValidator<IncomeDto>, IncomeDtoValidator>();
            services.AddTransient<IValidator<ExpenseItemDto>, ExpenseItemDtoValidator>();
            services.AddTransient<IValidator<ExpenseDto>, ExpenseDtoValidator>();

            services.AddScoped<IValidatorWrapper, ValidatorWrapper>();

            services.AddSingleton<IValidatorFactory, ServiceProviderValidatorFactory>();
        }
    }
}
