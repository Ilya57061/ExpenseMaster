
namespace ExpenseMaster.Model.Models
{
    public class FinancialGoal
    {
        public int Id { get; set; }
        public string GoalName { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; }

        public User? User { get; set; }
    }
}
