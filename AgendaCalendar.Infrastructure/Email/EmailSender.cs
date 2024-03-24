using AgendaCalendar.Application.Common.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace AgendaCalendar.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        public async Task SendMessageAsync(string address, string subject, string message_body, CancellationToken cancellationToken = default)
        {
            SmtpClient smtp = new();
            smtp.Connect("smtp.yandex.ru", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("agendacalendar@yandex.ru", "qchwnwthulwrsirq");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("agendacalendar@yandex.ru"));
            email.To.Add(new MailboxAddress("", address));
            email.Subject = subject;
            BodyBuilder messageBody = new BodyBuilder();
            messageBody.HtmlBody = message_body;

            email.Body = messageBody.ToMessageBody();

            var text = await smtp.SendAsync(email, cancellationToken);
            Console.WriteLine("Email sended!");
            smtp.Disconnect(true);
        }
    }
}
