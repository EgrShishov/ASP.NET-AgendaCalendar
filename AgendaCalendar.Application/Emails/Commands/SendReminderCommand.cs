using AgendaCalendar.Application.Common.Interfaces;

namespace AgendaCalendar.Application.Emails.Commands
{
    public sealed record SendReminderCommand(Reminder reminder) : IRequest<bool> { }

    public class SendReminderCommandHandler(IUnitOfWork unitOfWork, IEmailSender emailSender) : IRequestHandler<SendReminderCommand, bool>
    {
        public async Task<bool> Handle(SendReminderCommand request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.UserRepository.GetListAsync();
            var userName = "dear";
            if (users != null) userName = users.First().UserName; 
            // var user = users.Where(x => x.Email == request.reminder.Email).First();
            var message_body = "<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Reminder</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f9f9f9;\r\n            color: #333333;\r\n            margin: 0;\r\n            padding: 0;\r\n        }\r\n        .container {\r\n            max-width: 600px;\r\n            margin: 20px auto;\r\n            padding: 20px;\r\n            background-color: #ffa812;\r\n            border-radius: 10px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n        }\r\n        h1 {\r\n            font-size: 24px;\r\n            font-weight: bold;\r\n        }\r\n        p {\r\n            font-size: 16px;\r\n            line-height: 1.5;\r\n        }\r\n    </style>\r\n</head>\r\n"
                + $"<body>\r\n    <div class=\"container\">\r\n        <h1 align = center>Hey, {userName}!</h1>\r\n        <p>I hasten to remind you that the <strong>{request.reminder.Description}</strong> event will take place in just an hour. <b>Don't miss it!</b></p>\r\n    </div>\r\n</body>";
            await emailSender.SendMessageAsync(request.reminder.Email, "Dont miss your event!", message_body);
            return true;
        }
    }
}
