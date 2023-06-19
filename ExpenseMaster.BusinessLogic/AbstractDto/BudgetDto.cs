namespace ExpenseMaster.BusinessLogic.AbstractDto
{
    public abstract class BudgetDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public decimal WarningThreshold { get; set; }
    }
}
