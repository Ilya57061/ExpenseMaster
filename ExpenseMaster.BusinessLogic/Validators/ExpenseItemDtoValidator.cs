using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Validators.AbstractValidator;
using FluentValidation;

namespace ExpenseMaster.BusinessLogic.Validators
{
    public class ExpenseItemDtoValidator : TransactionDtoValidator<ExpenseItemDto>
    {
        public ExpenseItemDtoValidator()
        {
            RuleFor(dto => dto.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
