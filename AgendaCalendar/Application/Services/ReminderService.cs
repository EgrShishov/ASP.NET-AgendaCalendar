using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Entities;
using AgendaCalendar.Persistence.Repository;

namespace AgendaCalendar.Application.Services
{
    public class ReminderService : IService<Reminder>
    {
        private ReminderRepository reminderRepository;
        private MailService mailService;
        private Timer ReminderTimer;

        public ReminderService(ReminderRepository reminderRepository) 
        {
            this.reminderRepository = reminderRepository;

            mailService = new();
            ReminderTimer = new(CheckAndSendReminders, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async void CheckAndSendReminders(object state)
        {
            DateTime currentTime = DateTime.Now;
            List<Reminder> remindersToSend = new List<Reminder>();

            foreach(var reminder in await reminderRepository.GetListAsync())
            {
                if(reminder.ReminderTime - currentTime < TimeSpan.FromHours(1))
                {
                    remindersToSend.Add(reminder);
                }
            }

            foreach(var reminder in remindersToSend) 
            {
                await mailService.SendEmailAsync(reminder);
                //await reminderRepository.DeleteAsync(reminder.Id);
            }
        }
/*        public void CreateReminder()
        {
            throw new NotImplementedException();
        }

        public void RemoveReminder()
        {
            throw new NotImplementedException();
        }

        public void EditReminder()
        {
            throw new NotImplementedException();
        }

        public void GetReminderById()
        {
            throw new NotImplementedException();
        }

        public void GetAllReminders()
        {
            throw new NotImplementedException();
        }

        public void GetUpcomingReminders()
        {
            throw new NotImplementedException();
        }*/

        public async Task<IEnumerable<Reminder>> GetAllAsync()
        {
            return await reminderRepository.GetListAsync();
        }

        public async Task<Reminder> GetByIdAsync(int id)
        {
            return await reminderRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await reminderRepository.DeleteAsync(id);
        }

        public async Task AddAsync(Reminder item)
        {
            await reminderRepository.AddAsync(item);
        }

        public async Task<Reminder> UpdateAsync(Reminder item)
        {
            throw new NotImplementedException();
        }
    }
}