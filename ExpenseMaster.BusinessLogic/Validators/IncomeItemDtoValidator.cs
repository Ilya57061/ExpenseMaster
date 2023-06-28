using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Validators.AbstractValidator;
using FluentValidation;

namespace ExpenseMaster.BusinessLogic.Validators
{
    public class IncomeItemDtoValidator : TransactionDtoValidator<IncomeItemDto>
    {
        public IncomeItemDtoValidator()
        {
            RuleFor(dto => dto.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}
