﻿namespace ExpenseMaster.BusinessLogic.Dto
{
    public class ExpenseDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
