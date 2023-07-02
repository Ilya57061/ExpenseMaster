using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Validators.AbstractValidator;

namespace ExpenseMaster.BusinessLogic.Validators
{
    public class ExpenseDtoValidator : TransactionDtoValidator<ExpenseDto>
    {
        public ExpenseDtoValidator()
        {
        }
    }
}
