using AgendaCalendar.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AgendaCalendar.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        public EmailSender(IOptions<EmailSettings> settings) 
        {
            emailSettings = settings.Value;
        }
        public async Task SendMessageAsync(string address, string subject, string message_body, CancellationToken cancellationToken = default)
        {
            SmtpClient smtp = new();
            smtp.Connect("smtp.yandex.ru", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("agendacalendar@yandex.ru", "qchwnwthulwrsirq");
            //smtp.Connect(emailSettings.Host, emailSettings.Port, MailKit.Security.SecureSocketOptions.SslOnConnect); only for ASP .NET
            //smtp.Authenticate(emailSettings.ServiceEmail, emailSettings.Password);

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("agendacalendar@yandex.ru"));
            email.To.Add(new MailboxAddress("", address));
            email.Subject = subject;
            BodyBuilder messageBody = new BodyBuilder();
            messageBody.HtmlBody = message_body;

            email.Body = messageBody.ToMessageBody();

            await smtp.SendAsync(email, cancellationToken);
            smtp.Disconnect(true);
        }
    }
}
