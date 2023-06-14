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
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var expenses = await _expenseService.GetAllExpenses();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense(CreateExpenseDto createExpenseDto)
        {
            var expense = await _expenseService.CreateExpense(createExpenseDto);
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> UpdateExpense(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest();
            }

            var existingExpense = await _expenseService.GetExpenseById(id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            var updatedExpense = await _expenseService.UpdateExpense(expense);
            return Ok(updatedExpense);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteExpense(int id)
        {
            var existingExpense = await _expenseService.GetExpenseById(id);
            if (existingExpense == null)
            {
                return NotFound();
            }

            await _expenseService.DeleteExpense(existingExpense);
            return Ok(existingExpense.Id);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByCategory(int categoryId)
        {
            var expenses = await _expenseService.GetExpensesByCategory(categoryId);
            return Ok(expenses);
        }

        [HttpGet("user/{userId}/total")]
        public async Task<ActionResult<decimal>> CalculateTotalExpenseByUserId(int userId)
        {
            var totalExpenses = await _expenseService.CalculateTotalExpensesByUserId(userId);
            return Ok(totalExpenses);
        }
    }
}
