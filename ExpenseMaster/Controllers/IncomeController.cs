using ExpenseMaster.BusinessLogic.Dto;
using ExpenseMaster.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseMaster.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeItemDto>>> GetIncomes()
        {
            var incomesItemDto = await _incomeService.GetAllIncomes();

            return Ok(incomesItemDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeItemDto>> GetIncomeById(int id)
        {
            var incomeItemDto = await _incomeService.GetIncomeById(id);

            return Ok(incomeItemDto);
        }

        [HttpPost]
        public async Task<ActionResult<IncomeItemDto>> CreateIncome(IncomeDto incomeDto)
        {
            var income = await _incomeService.CreateIncome(incomeDto);

            return Ok(income);
        }

        [HttpPut]
        public async Task<ActionResult<IncomeItemDto>> UpdateIncome(IncomeItemDto incomeItemDto)
        {
            var income = await _incomeService.UpdateIncome(incomeItemDto);

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
