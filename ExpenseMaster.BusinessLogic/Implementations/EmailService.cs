using ExpenseMaster.BusinessLogic.Interfaces;
using ExpenseMaster.DAL.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ExpenseMaster.BusinessLogic.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly string _port;
        private readonly string _host;
        private readonly string _name;
        private readonly string _emailAddress;
        private readonly string _emailPassword;

        public EmailService(IConfiguration configuration)
        {
            _host = configuration["Email:Host"];
            _port = configuration["Email:Port"];
            _name = configuration["Email:Name"];
            _emailAddress = configuration["Email:Address"];
            _emailPassword = configuration["Email:Password"];
        }

        public async Task SendEmailAsync(string email, Notification notification)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_name, _emailAddress));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = notification.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = notification.Message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_host, int.Parse(_port), true);
                await client.AuthenticateAsync(_emailAddress, _emailPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
