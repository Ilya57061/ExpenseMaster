
namespace ExpenseMaster.Model.Models
{
    public class Income
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public User? User { get; set; }
        public Category? Category { get; set; }
    }
}
