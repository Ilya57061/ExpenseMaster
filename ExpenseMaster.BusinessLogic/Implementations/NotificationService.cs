using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task SendBalanceLowNotification(string userEmail)
        {
            var notificationMessage = "Ваш баланс находится на исходе. Пожалуйста, пополните счет.";
            var notificationSubject = "Уведомление: Баланс на исходе";
            var notification = new Notification { Message = notificationMessage, Subject = notificationSubject };
            await _emailService.SendEmailAsync(userEmail, notification);
        }
        public async Task SendInsufficientFundsNotification(string userEmail)
        {
            var notificationMessage = "Недостаточно средств на вашем счете для выполнения операции.";
            var notificationSubject = "Уведомление: Недостаточно средств";
            var notification = new Notification { Message = notificationMessage, Subject = notificationSubject };
            await _emailService.SendEmailAsync(userEmail, notification);
        }
        public async Task SendRechargeNotification(string userEmail, decimal amount)
        {
            string notificationMessage = $"Ваш счет был пополнен на сумму {amount}.";
            var notificationSubject = "Уведомление: Пополнение счета";
            var notification = new Notification { Message = notificationMessage, Subject = notificationSubject };
            await _emailService.SendEmailAsync(userEmail, notification);
        }
    }
}
