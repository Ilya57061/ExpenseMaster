using ExpenseMaster.Model.Models;

namespace ExpenseMaster.BusinessLogic.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, Notification notification);
    }
}
