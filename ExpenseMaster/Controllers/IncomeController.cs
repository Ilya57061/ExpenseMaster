using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("public")]
    [AllowAnonymous]
    public class IncomeController : Controller
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public IncomeController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetIncome()
        {
            var incomes = await _repositoryWrapper.Income.FindAllAsync();
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            if(income == null)
            {
                return NotFound();
            }

            return Ok(income);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateIncome(Income income)
        {
            if(income == null)
            {
                return BadRequest();
            }

            await _repositoryWrapper.Income.CreateAsync(income);
            await _repositoryWrapper.SaveAsync();

            return CreatedAtAction(nameof(GetIncomeById), new {id = income.Id}, income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, Income income)
        {
            if (income == null || income.Id != id)
            {
                return BadRequest();
            }

            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            if (existingIncome == null)
                return NotFound();

            await _repositoryWrapper.Income.UpdateAsync(income);
            await _repositoryWrapper.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            var existingIncome = await _repositoryWrapper.Income.FindByConditionAsync(x => x.Id == id);
            if (existingIncome == null)
            {
                return NotFound();
            }

            var incomeToDelete = existingIncome.FirstOrDefault();

            await _repositoryWrapper.Income.DeleteAsync(incomeToDelete);
            await _repositoryWrapper.SaveAsync();

            return NoContent();
        }

        [HttpGet("incomes/category/{categoryId}")]
        public async Task<IActionResult> GetIncomesByCategory(int categoryId)
        {
            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.CategoryId == categoryId);
            if (incomes == null)
            {
                return NotFound();
            }

            return Ok(incomes);
        }

        [HttpGet("incomes/user/{userId}/total")]
        public async Task<IActionResult> CalculateTotalIncomeByUserId(int userId)
        {
            var incomes = await _repositoryWrapper.Income.FindByConditionAsync(x => x.UserId == userId);
            if (incomes == null)
            {
                return NotFound();
            }

            decimal totalIncome = incomes.Sum(x => x.Amount);

            return Ok(totalIncome);
        }
    }
}
