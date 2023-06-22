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
            var notificationMessage = "Ваши расходы превышают доходы!";
            var notificationSubject = "Уведомление о расходах.";
            var notification = new Notification { Message = notificationMessage, Subject = notificationSubject };
            await _emailService.SendEmailAsync(userEmail, notification);
        }
    }
}
