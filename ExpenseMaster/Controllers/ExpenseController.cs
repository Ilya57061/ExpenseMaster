using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
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
        public async Task<ActionResult<IEnumerable<ExpenseWithIdDto>>> GetExpenses()
        {
            var expensesWithIdDto = await _expenseService.GetAllExpenses();

            return Ok(expensesWithIdDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseWithIdDto>> GetExpenseById(int id)
        {
            var expenseWithIdDto = await _expenseService.GetExpenseById(id);

            return Ok(expenseWithIdDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseDto>> CreateExpense(ExpenseDto expenseDto)
        {
            var expense = await _expenseService.CreateExpense(expenseDto);

            return Ok(expense);
        }

        [HttpPut]
        public async Task<ActionResult<ExpenseWithIdDto>> UpdateExpense(ExpenseWithIdDto expenseWithIdDto)
        {
            var expense = await _expenseService.UpdateExpense(expenseWithIdDto);

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
