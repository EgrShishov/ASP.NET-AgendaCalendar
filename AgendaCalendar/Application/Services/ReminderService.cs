using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Hangfire;

namespace AgendaCalendar.Application.Services
{
    public class ReminderService : IService<Reminder>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MailService mailService;

        public ReminderService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;

            mailService = new();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            RecurringJob.AddOrUpdate("CheckRemindersJob", () => CheckAndSendReminders(), Cron.MinuteInterval(1));
        }

        private async void CheckAndSendReminders()
        {
            DateTime currentTime = DateTime.Now;
            List<Reminder> remindersToSend = new List<Reminder>();

            foreach(var reminder in await unitOfWork.ReminderRepository.GetListAsync())
            {
                if(reminder.ReminderTime - currentTime < TimeSpan.FromHours(1))
                {
                    remindersToSend.Add(reminder);
                }
            }

            foreach(var reminder in remindersToSend) 
            {
                await mailService.SendReminderAsync(reminder);
                await unitOfWork.ReminderRepository.DeleteAsync(reminder.Id);
            }
        }
        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await unitOfWork.ReminderRepository.GetListAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await unitOfWork.ReminderRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.ReminderRepository.DeleteAsync(id);
        }

        public async Task AddAsync(Reminder item)
        {
            await unitOfWork.ReminderRepository.AddAsync(item);
        }

        public async Task<Reminder> UpdateAsync(Reminder item)
        {
            throw new NotImplementedException();
        }

        public async Task AddReminder(Reminder item, int calendarId)
        {
            if(item != null)
            {
                var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
                calendar.Reminders.Add(item);

                await unitOfWork.CalendarRepository.UpdateAsync(calendar);
                await unitOfWork.ReminderRepository.AddAsync(item);
            }
        }
    }
}