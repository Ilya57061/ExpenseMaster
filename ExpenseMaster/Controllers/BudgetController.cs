using ExpenseMaster.BusinessLogic.AbstractDto;
using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
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
        public async Task<ActionResult<BudgetDto>> GetByIdAsync(int id)
        {
            var budget = await _budgetService.GetByIdAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBudgetDto>> CreateAsync([FromBody] CreateBudgetDto budget)
        {
            await _budgetService.CreateAsync(budget);

            return Ok(budget);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateBudgetDto budget)
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetByUserIdAsync(int userId)
        {
            var budgets = await _budgetService.GetByUserIdAsync(userId);

            return Ok(budgets);
        }

        [HttpGet("{userId}/category/{categoryId}")]
        public async Task<ActionResult<BudgetDto>> GetByCategoryIdAsync(int userId, int categoryId)
        {
            var budget = await _budgetService.GetByCategoryIdAsync(userId, categoryId);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        [HttpGet("{userId}/exceeding-threshold")]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgetsExceedingThresholdAsync(int userId)
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

        [HttpGet("{userId}/remaining-amount")]
        public async Task<ActionResult<decimal>> GetBudgetRemainingAmountAsync(int userId)
        {
            var remainingAmount = await _budgetService.GetBudgetRemainingAmountAsync(userId);

            return Ok(remainingAmount);
        }
    }
}