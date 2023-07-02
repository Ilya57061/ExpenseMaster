using ExpenseMaster.BusinessLogic.Validators.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace ExpenseMaster.BusinessLogic.Validators.Wrapper
{
    public class ValidatorWrapper : IValidatorWrapper
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidatorWrapper(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public ValidationResult Validate<T>(T validate)
        {
            var validator = _validatorFactory.GetValidator<T>();

            return validator.Validate(validate);
        }
    }
}
