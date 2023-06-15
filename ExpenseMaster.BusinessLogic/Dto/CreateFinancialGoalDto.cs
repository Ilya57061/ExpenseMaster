using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Dto
{
    public class CreateFinancialGoalDto
    {
        public int UserId { get; set; }
        public string GoalName { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
