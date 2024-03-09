using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using AgendaCalendar.Persistence.Data;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace AgendaCalendar.Persistence.Repository
{
    public class ReminderRepository : IRepository<Reminder>
    {
        private readonly AppDbContext _dbContext;

        public ReminderRepository(AppDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task AddAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            _dbContext.Reminders.Add(reminder);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var reminderToDelete = _dbContext.Reminders.Find(x => x.Id == id);
            _dbContext.Reminders.Remove(reminderToDelete);
        }

        public async Task<Reminder> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Reminders.FirstOrDefault(x=>x.Id == id);
        }

        public async Task<IReadOnlyList<Reminder>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Reminders.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Reminder>> ListAsync(Expression<Func<Reminder, bool>> filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Reminders.AsQueryable();
            if(filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public async Task UpdateAsync(Reminder reminder, CancellationToken cancellationToken = default)
        {
            var myReminder = _dbContext.Reminders.FirstOrDefault(x => x.Id == reminder.Id);
            myReminder.Update(reminder);
        }
    }
}