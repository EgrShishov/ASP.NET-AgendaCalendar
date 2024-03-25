using AgendaCalendar.Application.Common.Interfaces;

namespace AgendaCalendar.Application.Reminders.Commands
{
    public sealed record SendBirthdayRemindersCommand() : IRequest<IReadOnlyList<User>> { }

    public class SendBirthdayRemindersCommandHandler(IUnitOfWork unitOfWork, IEmailSender emailSender) : IRequestHandler<SendBirthdayRemindersCommand, IReadOnlyList<User>>
    {
        public async Task<IReadOnlyList<User>> Handle(SendBirthdayRemindersCommand request, CancellationToken cancellationToken)
        {
            var usersWithBirthdayToday = await unitOfWork.UserRepository.ListAsync(x => x.BirthdayDate.Date == DateTime.UtcNow.Date);
            if (usersWithBirthdayToday == null) return null;

            foreach(var user in usersWithBirthdayToday)
            {
                var message_body = $"happy birthday, {user.UserName}!";
                var subject = "It's your birthday!";
                await emailSender.SendMessageAsync(user.Email, subject, message_body);
            }
            return usersWithBirthdayToday;
        }
    }
}
