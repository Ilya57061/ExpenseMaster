﻿namespace ExpenseMaster.Model.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
