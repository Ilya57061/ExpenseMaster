using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
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
        public async Task<ActionResult<IEnumerable<IncomeWithIdDto>>> GetIncomes()
        {
            var incomesWithIdDto = await _incomeService.GetAllIncomes();

            return Ok(incomesWithIdDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeWithIdDto>> GetIncomeById(int id)
        {
            var incomeWithIdDto = await _incomeService.GetIncomeById(id);

            return Ok(incomeWithIdDto);
        }

        [HttpPost]
        public async Task<ActionResult<IncomeDto>> CreateIncome(IncomeDto incomeDto)
        {
            var income = await _incomeService.CreateIncome(incomeDto);

            return Ok(income);
        }

        [HttpPut]
        public async Task<ActionResult<IncomeWithIdDto>> UpdateIncome(IncomeWithIdDto incomeWithIdDto)
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
        public async Task<ActionResult<IEnumerable<IncomeDto>>> GetIncomesByCategory(int categoryId)
        {
            var incomesDto = await _incomeService.GetIncomesByCategory(categoryId);

            return Ok(incomesDto);
        }

        [HttpGet("user/{userId}/total")]
        public async Task<ActionResult<decimal>> CalculateTotalIncomeByUserId(int userId)
        {
            var totalIncomes = await _incomeService.CalculateTotalIncomeByUserId(userId);

            return Ok(totalIncomes);
        }
    }
}
