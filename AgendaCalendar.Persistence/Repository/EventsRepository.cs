using AgendaCalendar.Persistence.Data;
using AgendaCalendar.Domain.Abstractions;
using System.Linq.Expressions;

namespace AgendaCalendar.Persistence.Repository
{
    public class EventsRepository : IRepository<IEvent>
    {
        private readonly AppDbContext _dbContext;

        public EventsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(IEvent ev, CancellationToken cancellationToken = default)
        {
            _dbContext.Events.Add(ev);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var eventToDelete = _dbContext.Events.Find(x => x.Id == id);
            _dbContext.Events.Remove(eventToDelete);
        }

        public async Task<IEvent> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.Events.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IReadOnlyList<IEvent>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Events.ToList().AsReadOnly();
        }

        public async Task<IReadOnlyList<IEvent>> ListAsync(Expression<Func<IEvent, bool>> filter, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Events.AsQueryable();
            if (filter != null) query = query.Where(filter);

            return query.ToList();
        }

        public async Task<IEvent> UpdateAsync(IEvent ev, CancellationToken cancellationToken = default)
        {
            var MyEvent = _dbContext.Events.FirstOrDefault(x => x.Id == ev.Id);
            MyEvent?.Update(ev);
            return MyEvent;
            //state modified
        }
    }
}