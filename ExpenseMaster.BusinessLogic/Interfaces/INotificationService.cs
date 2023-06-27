namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task SendExpenseExceededNotification(string userEmail);
    }
}
