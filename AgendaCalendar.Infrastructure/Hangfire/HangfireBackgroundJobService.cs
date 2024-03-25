using AgendaCalendar.Application.Emails.Commands;
using Hangfire;
using MediatR;

namespace AgendaCalendar.Infrastructure.Hangfire
{
    public class HangfireBackgroundJobService
    {
       /* public HangfireBackgroundJobService(IUnitOfWork unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            Console.WriteLine("Hangfire background job service started...");
        }

        public void ScheduleReminderJob(Reminder reminder)
        {
            //RecurringJob.AddOrUpdate("my-recurring-job",() => CheckRemindersForSending(), Cron.MinuteInterval(1));
            Console.WriteLine("sended");
        }

        public async Task<IReadOnlyList<Reminder>> CheckRemindersForSending()
        {
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
                //await mediator.Send(new SendReminderCommand(reminder));
                await unitOfWork.ReminderRepository.DeleteAsync(reminder.Id);
            }

            await unitOfWork.SaveAllAsync();
            return remindersToSend;
        }*/
    }
}
