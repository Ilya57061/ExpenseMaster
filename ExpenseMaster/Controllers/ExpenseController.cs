using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("public/expenses")]
    [AllowAnonymous]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseItemDto>>> GetExpenses()
        {
            var expensesItemDto = await _expenseService.GetAllExpenses();

            return Ok(expensesItemDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseItemDto>> GetExpenseById(int id)
        {
            var expenseItemDto = await _expenseService.GetExpenseById(id);

            return Ok(expenseItemDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseItemDto>> CreateExpense(ExpenseDto expenseDto)
        {
            var expense = await _expenseService.CreateExpense(expenseDto);

            return Ok(expense);
        }

        [HttpPut]
        public async Task<ActionResult<ExpenseItemDto>> UpdateExpense(ExpenseItemDto expenseItemDto)
        {
            var expense = await _expenseService.UpdateExpense(expenseItemDto);

            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteExpense(int id)
        {
            var existingExpense = await _expenseService.GetExpenseById(id);
            await _expenseService.DeleteExpense(existingExpense);

            return Ok(existingExpense.Id);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ExpenseDto>>> GetExpensesByCategory(int categoryId)
        {
            var expensesDto = await _expenseService.GetExpensesByCategory(categoryId);

            return Ok(expensesDto);
        }

        [HttpGet("user/{userId}/total")]
        public async Task<ActionResult<decimal>> CalculateTotalExpenseByUserId(int userId)
        {
            var totalExpenses = await _expenseService.CalculateTotalExpensesByUserId(userId);

            return Ok(totalExpenses);
        }
    }
}
