using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseMaster.Model.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId {get; set;}
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal Limit { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency)]
        public decimal WarningThreshold { get; set; }
    }
}
