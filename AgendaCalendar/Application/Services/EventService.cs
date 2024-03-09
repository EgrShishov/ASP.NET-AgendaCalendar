using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Persistence.Repository;

namespace AgendaCalendar.Application.Services
{
    public class EventService : IService<IEvent>
    {
        private EventsRepository eventsRepository;

        public EventService(EventsRepository eventsRepository) 
        {
            this.eventsRepository = eventsRepository;
        }
/*        public void AddEvent()
        {
            throw new NotImplementedException();
        }

        public void DeleteEvent()
        {
            throw new NotImplementedException();
        }

        public void EditEvent()
        {
            throw new NotImplementedException();
        }

        public void GetUserEvents()
        {
            throw new NotImplementedException();
        }

        public void GetEvents()
        {
            throw new NotImplementedException();
        }

        public void AddRecurringEvent()
        {
            throw new NotImplementedException();
        }

        public void AddReminder()
        {
            throw new NotImplementedException();
        }*/

        public async Task<IEnumerable<IEvent>> GetAllAsync()
        {
            return await eventsRepository.GetListAsync();
        }

        public async Task<IEvent> GetByIdAsync(int id)
        {
            return await eventsRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await eventsRepository.DeleteAsync(id);
        }

        public async Task AddAsync(IEvent item)
        {
            await eventsRepository.AddAsync(item);
        }

        public Task<IEvent> UpdateAsync(IEvent item)
        {
            throw new NotImplementedException();
        }
    }
}