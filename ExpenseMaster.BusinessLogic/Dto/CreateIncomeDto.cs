namespace ExpenseMaster.BusinessLogic.Dto
{
    public class CreateIncomeDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
