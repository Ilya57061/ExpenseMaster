using FluentValidation.Results;

namespace ExpenseMaster.BusinessLogic.Validators.Interfaces
{
    public interface IValidatorWrapper
    {
        ValidationResult Validate<T>(T validate);
    }
}
