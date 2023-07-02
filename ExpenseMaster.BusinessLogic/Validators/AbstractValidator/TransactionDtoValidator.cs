using ExpenseMaster.BusinessLogic.AbstractDto;
using FluentValidation;

namespace ExpenseMaster.BusinessLogic.Validators.AbstractValidator
{
    public abstract class TransactionDtoValidator<T> : AbstractValidator<T> where T : TransactionDto
    {
        public TransactionDtoValidator()
        {
            RuleFor(dto => dto.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(dto => dto.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0");
            RuleFor(dto => dto.CategoryId).GreaterThan(0).WithMessage("CategoryId must be greater than 0");
            RuleFor(dto => dto.Date).NotEmpty().WithMessage("Date is required").LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future");
        }
    }
}
