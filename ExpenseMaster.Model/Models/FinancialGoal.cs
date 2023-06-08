using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMaster.Model.Models
{
    public class FinancialGoal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [Required]
        public string GoalName { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal TargetAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal CurrentAmount { get; set; }
    }
}
