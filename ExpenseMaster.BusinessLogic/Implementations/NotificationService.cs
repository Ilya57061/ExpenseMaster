using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendExpenseExceededNotification(string userEmail)
        {
            var notificationMessage = "Ваш баланс находится на исходе. Пожалуйста, пополните счет.";
            var notificationSubject = "Уведомление: Баланс на исходе";
            var notification = new Notification { Message = notificationMessage, Subject = notificationSubject };
            await _emailService.SendEmailAsync(userEmail, notification);
        }
    }
}
