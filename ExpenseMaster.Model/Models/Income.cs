

using System.Reflection.Metadata;

namespace ExpenseMaster.Model.Models
{
    public class Income
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
