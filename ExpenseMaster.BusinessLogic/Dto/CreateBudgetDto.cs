namespace ExpenseMaster.BusinessLogic.Dto
{
    public class CreateBudgetDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public decimal WarningThreshold { get; set; }
    }
}
