using AgendaCalendar.Domain.Entities;
using MailKit.Net.Smtp;
using MimeKit;

namespace AgendaCalendar.Application.Services
{
    public class MailService
    {
        private SmtpClient smtp;

        public MailService()
        {

        }

        public async Task SendReminderAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            smtp = new();
            smtp.Connect("smtp.yandex.ru", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("agendacalendar@yandex.ru", "qchwnwthulwrsirq");

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("agendacalendar@yandex.ru"));
            email.To.Add(new MailboxAddress("", reminder.Email));
            email.Subject = "Dont miss your event!";
            email.Body = new TextPart("plain")
            {
                Text = $"Hey! I hasten to remind you that the {reminder.Description} event will take place in just an hour. Don't miss it!"
            };

            await smtp.SendAsync(email, cancellationToken);
            Console.WriteLine("Email sended!");

            smtp.Disconnect(true);
        }
    }
}
