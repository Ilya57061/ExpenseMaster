namespace ExpenseMaster.Model.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
