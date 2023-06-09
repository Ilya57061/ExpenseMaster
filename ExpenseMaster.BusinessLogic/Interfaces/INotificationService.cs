namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface INotificationService
    {
        Task SendBalanceLowNotification(string userEmail);
        Task SendInsufficientFundsNotification(string userEmail);
        Task SendRechargeNotification(string userEmail, decimal amount);
    }
}
