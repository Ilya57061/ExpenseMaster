
namespace ExpenseMaster.Model.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Limit { get; set; }
        public decimal WarningThreshold { get; set; }

        //связь с user
    }
}
