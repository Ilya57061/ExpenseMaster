namespace ExpenseMaster.BusinessLogic.AbstractDto
{
    public abstract class BaseFinancialGoalDto
    {
        public int UserId { get; set; }
        public string GoalName { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
