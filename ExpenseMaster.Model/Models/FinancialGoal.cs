namespace ExpenseMaster.DAL.Models
{
    public class FinancialGoal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public string GoalName { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }
    }
}
