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
    public class FinancialGoalController : Controller
    {
        private readonly IFinancialGoalService _financialGoalService;

        public FinancialGoalController(IFinancialGoalService financialGoalService)
        {
            _financialGoalService = financialGoalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFinancialGoalDto financialGoalDto)
        {
            await _financialGoalService.CreateAsync(financialGoalDto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var financialGoal = await _financialGoalService.GetByIdAsync(id);

            if (financialGoal == null)
            {
                return NotFound();
            }

            await _financialGoalService.DeleteAsync(financialGoal);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialGoal>> GetById(int id)
        {
            var financialGoal = await _financialGoalService.GetByIdAsync(id);

            if (financialGoal == null)
            {
                return NotFound();
            }

            return financialGoal;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<FinancialGoal>>> GetByUserId(int userId)
        {
            var financialGoals = await _financialGoalService.GetByUserIdAsync(userId);
            return Ok(financialGoals);
        }

        [HttpGet("{userId}/reached")]
        public async Task<ActionResult<IEnumerable<FinancialGoal>>> GetReachedGoalsByUserId(int userId)
        {
            var reachedGoals = await _financialGoalService.GetByTargetAmountAsync(userId);

            return Ok(reachedGoals);
        }

        [HttpGet("{userId}/progress")]
        public async Task<ActionResult<decimal>> GetTotalProgressByUserId(int userId)
        {
            var totalProgress = await _financialGoalService.GetTotalProgressAsync(userId);

            return Ok(totalProgress);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFinancialGoalDto financialGoalDto)
        {
            if (id != financialGoalDto.Id)
            {
                return BadRequest();
            }

            var existingGoal = await _financialGoalService.GetByIdAsync(id);

            if (existingGoal == null)
            {
                return NotFound();
            }

            await _financialGoalService.UpdateAsync(financialGoalDto);

            return Ok();
        }

        [HttpPatch("{id}/currentAmount")]
        public async Task<IActionResult> UpdateCurrentAmount(int id, [FromBody] UpdateCurrentAmountDto updateCurrentAmountDto)
        {
            if (id != updateCurrentAmountDto.Id)
            {
                return BadRequest();
            }

            var existingGoal = await _financialGoalService.GetByIdAsync(id);

            if (existingGoal == null)
            {
                return NotFound();
            }

            await _financialGoalService.UpdateCurrentAmountAsync(id, updateCurrentAmountDto.CurrentAmount);

            return Ok();
        }
    }
}
