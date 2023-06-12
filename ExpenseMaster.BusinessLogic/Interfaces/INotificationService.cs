namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task SendBalanceLowNotification(string userEmail);
    }
}
