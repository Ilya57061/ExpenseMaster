using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("public")]
    [AllowAnonymous]
    public class BudgetController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Budget>> GetByIdAsync(int id)
        {
            var budget = await _budgetService.GetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Budget budget)
        {
            await _budgetService.CreateAsync(budget);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = budget.Id }, budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            await _budgetService.UpdateAsync(budget);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var budget = await _budgetService.GetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            await _budgetService.DeleteAsync(budget);

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Budget>>> GetByUserIdAsync(int userId)
        {
            var budgets = await _budgetService.GetByUserIdAsync(userId);

            return Ok(budgets);
        }

        [HttpGet("user/{userId}/category/{categoryId}")]
        public async Task<ActionResult<Budget>> GetByCategoryIdAsync(int userId, int categoryId)
        {
            var budget = await _budgetService.GetByCategoryIdAsync(userId, categoryId);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        [HttpGet("user/{userId}/exceeding-threshold")]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgetsExceedingThresholdAsync(int userId)
        {
            var budgets = await _budgetService.GetBudgetsExceedingThresholdAsync(userId);

            return Ok(budgets);
        }

        [HttpPut("{budgetId}/warning-threshold")]
        public async Task<IActionResult> UpdateWarningThresholdAsync(int budgetId, [FromBody] decimal warningThreshold)
        {
            await _budgetService.UpdateWarningThresholdAsync(budgetId, warningThreshold);

            return NoContent();
        }

        [HttpGet("user/{userId}/remaining-amount")]
        public async Task<ActionResult<decimal>> GetBudgetRemainingAmountAsync(int userId)
        {
            var remainingAmount = await _budgetService.GetBudgetRemainingAmountAsync(userId);

            return Ok(remainingAmount);
        }
    }
}
