using AgendaCalendar.Application.Services;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Reminders.Commands
{
    public sealed record CheckRemindersCommand() : IRequest<IReadOnlyList<Reminder>> { }

    public class CheckRemindersCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CheckRemindersCommand, IReadOnlyList<Reminder>>
    {
        public async Task<IReadOnlyList<Reminder>> Handle(CheckRemindersCommand request, CancellationToken cancellationToken)
        {
            var mailService = new MailService();
            DateTime currentTime = DateTime.Now;
            List<Reminder> remindersToSend = new List<Reminder>();

            foreach (var reminder in await unitOfWork.ReminderRepository.GetListAsync())
            {
                if (reminder.ReminderTime - currentTime < TimeSpan.FromHours(1))
                {
                    remindersToSend.Add(reminder);
                }
            }
            Console.WriteLine(remindersToSend.Count);
            foreach (var reminder in remindersToSend)
            {
                //await mailService.SendReminderAsync(reminder);
                await unitOfWork.ReminderRepository.DeleteAsync(reminder.Id);
            }

            await unitOfWork.SaveAllAsync();
            return remindersToSend;
        }
    }
}
