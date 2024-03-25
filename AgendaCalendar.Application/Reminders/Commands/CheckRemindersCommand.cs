using AgendaCalendar.Application.Emails.Commands;

namespace AgendaCalendar.Application.Reminders.Commands
{
    public sealed record CheckRemindersCommand() : IRequest<IReadOnlyList<Reminder>> { }

    public class CheckRemindersCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<CheckRemindersCommand, IReadOnlyList<Reminder>>
    {
        public async Task<IReadOnlyList<Reminder>> Handle(CheckRemindersCommand request, CancellationToken cancellationToken)
        {
            DateTime currentTime = DateTime.Now;
            List<Reminder> remindersToSend = new List<Reminder>();

            foreach (var reminder in await unitOfWork.ReminderRepository.GetListAsync())
            {
                if (reminder.ReminderTime - currentTime < reminder.NotificationInterval)
                {
                    remindersToSend.Add(reminder);
                }
            }
            if (remindersToSend is not null)
            {
                foreach (var reminder in remindersToSend)
                {
                    await mediator.Send(new SendReminderCommand(reminder));
                    await unitOfWork.ReminderRepository.DeleteAsync(reminder.Id);
                }
                await unitOfWork.SaveAllAsync();
            }
            return remindersToSend;
        }
    }
}
