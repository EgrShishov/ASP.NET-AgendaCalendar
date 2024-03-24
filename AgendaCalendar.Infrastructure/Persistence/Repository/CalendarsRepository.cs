using AgendaCalendar.Infrastructure.Persistence.Data;
using System.Linq.Expressions;

namespace AgendaCalendar.Infrastructure.Persistence.Repository
{
    public class CalendarRepository : IRepository<Calendar>
    {
        private readonly AppDbContext _dbContext;

        public CalendarRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Calendar calendar, CancellationToken cancellationToken = default)
        {
            _dbContext.Calendars.Add(calendar);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var calendarToDelete = _dbContext.Calendars.Find(x => x.Id == id);
            _dbContext.Calendars.Remove(calendarToDelete);
        }

        public async Task<Calendar> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Calendars.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Calendar>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Calendars.ToList().ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<Calendar>> ListAsync(Expression<Func<Calendar, bool>> filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Calendars.AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public async Task<Calendar> UpdateAsync(Calendar calendar, CancellationToken cancellationToken = default)
        {
            var myCalendar = _dbContext.Calendars.Find(x => x.Id == calendar.Id);
            myCalendar.Update(calendar);
            return myCalendar;
        }
    }
}