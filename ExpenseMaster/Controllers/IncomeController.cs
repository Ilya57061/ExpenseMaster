using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("public/incomes")]
    [AllowAnonymous]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var incomes = await _incomeService.GetAllIncomes();

            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncomeById(int id)
        {
            var income = await _incomeService.GetIncomeById(id);

            return Ok(income);
        }

        [HttpPost]
        public async Task<ActionResult<Income>> CreatIncome(IncomeDto incomeDto)
        {
            var income = await _incomeService.CreateIncome(incomeDto);

            return Ok(income);
        }

        [HttpPut]
        public async Task<ActionResult<Income>> UpdateIncome(IncomeWithIdDto incomeWithIdDto)
        {
            var income = await _incomeService.UpdateIncome(incomeWithIdDto);

            return Ok(income);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteIncome(int id)
        {
            var existingIncome = await _incomeService.GetIncomeById(id);
            await _incomeService.DeleteIncome(existingIncome);

            return Ok(existingIncome.Id);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomesByCategory(int categoryId)
        {
            var incomes = await _incomeService.GetIncomesByCategory(categoryId);

            return Ok(incomes);
        }

        [HttpGet("user/{userId}/total")]
        public async Task<ActionResult<decimal>> CalculateTotalIncomeByUserId(int userId)
        {
            var totalIncomes = await _incomeService.CalculateTotalIncomeByUserId(userId);

            return Ok(totalIncomes);
        }
    }
}
