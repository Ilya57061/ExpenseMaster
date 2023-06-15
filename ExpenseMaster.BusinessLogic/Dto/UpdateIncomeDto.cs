namespace ExpenseMaster.BusinessLogic.Dto
{
    public class UpdateIncomeDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
