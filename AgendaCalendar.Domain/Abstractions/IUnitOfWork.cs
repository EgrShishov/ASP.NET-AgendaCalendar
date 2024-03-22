using AgendaCalendar.Domain.Entities;

namespace AgendaCalendar.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Calendar> CalendarRepository { get; }
        IRepository<IEvent> EventRepository { get; }
        IRepository<Reminder> ReminderRepository { get; }
        IRepository<User> UserRepository { get; }
        public Task DeleteDataBaseAsync();
        public Task CreateDataBaseAsync();
        public Task SaveAllAsync();
    }
}
