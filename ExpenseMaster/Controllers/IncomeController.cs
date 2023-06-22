using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIncome()
        {
            var incomes = await _incomeService.GetAllIncomes();

            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIncomeById(int id)
        {
            var income = await _incomeService.GetIncomeById(id);
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

            await _incomeService.CreateIncome(income);

            return Ok(income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, Income income)
        {
            if (income == null || income.Id != id)
            {
                return BadRequest();
            }

            var existingIncome = await _incomeService.GetIncomeById(id);
            if (existingIncome == null)
            {
                return NotFound();
            }

            await _incomeService.UpdateIncome(existingIncome);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)   
        {
            var existingIncome = await _incomeService.GetIncomeById(id);
            if (existingIncome == null)
            {
                return NotFound();
            }

            await _incomeService.DeleteIncome(existingIncome);

            return NoContent();
        }

        [HttpGet("incomes/category/{categoryId}")]
        public async Task<IActionResult> GetIncomesByCategory(int categoryId)
        {
            var incomes = await _incomeService.GetIncomesByCategory(categoryId);
            return Ok(incomes);
        }

        [HttpGet("incomes/user/{userId}/total")]
        public async Task<IActionResult> CalculateTotalIncomeByUserId(int userId)
        {
            var totalIncome = await _incomeService.CalculateTotalIncomeByUserId(userId);
            return Ok(totalIncome);
        }
    }
}
