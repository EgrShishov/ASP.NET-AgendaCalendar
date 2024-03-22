using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;

namespace AgendaCalendar.Application.Services
{
    public class EventService : IService<IEvent>
    {
        private IUnitOfWork unitOfWork;
        private MailService mailService;

        public EventService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
            mailService = new();
        }

        public async Task<IEnumerable<IEvent>> GetAllAsync()
        {
            return await unitOfWork.EventRepository.GetListAsync();
        }

        public async Task<IEnumerable<IEvent>> GetAllEventsByRange(DateTime startDate, DateTime endDate)
        {
            return await unitOfWork.EventRepository.ListAsync(x => x.StartTime.Date >= startDate.Date && x.EndTime.Date <= endDate.Date);
        }

        public async Task<IEnumerable<IEvent>> GetAllEventsByDate(DateTime date)
        {
            return await unitOfWork.EventRepository.ListAsync(x => x.StartTime.Date == date.Date);
        }

        public async Task<IEnumerable<IEvent>> GetAllEventsForDateRange(int calendarId, DateTime startDate, DateTime endDate)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
            if(calendar is not null)
                return calendar.Events.Where(x => x.StartTime.Date >= startDate.Date && x.EndTime.Date <= endDate.Date);
            return null;
        }

        public async Task<IEnumerable<IEvent>> GetAllEventsForDate(int calendarId, DateTime date)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
            if (calendar is not null)
                return calendar.Events.Where(x => x.StartTime.Date == date.Date);
            return null;
        }

        public async Task<IEnumerable<IEvent>> GetAllEventsWithParticipant(EventParticipant eventParticipant)
        {
            return await unitOfWork.EventRepository.ListAsync(x => x.EventParticipants.Contains(eventParticipant));
        }

        public async Task AddEventParticipant(int id, EventParticipant eventParticipant)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(id);
            if (@event == null) return;

            @event.AddParticipant(eventParticipant);
            await unitOfWork.EventRepository.UpdateAsync(@event);
            await unitOfWork.SaveAllAsync();
        }

        public async Task RemoveEventParticipant(int id, EventParticipant eventParticipant)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(id);
            if(@event == null) return;

            @event.RemoveParticipant(eventParticipant);
            await unitOfWork.EventRepository.UpdateAsync(@event);
            await unitOfWork.SaveAllAsync();
        }
        public async Task<IEvent> GetByIdAsync(int id)
        {
            return await unitOfWork.EventRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await unitOfWork.EventRepository.GetByIdAsync(id);
            if (item == null) return;
            await unitOfWork.EventRepository.DeleteAsync(id);
            await unitOfWork.SaveAllAsync();
        }

        public async Task AddAsync(IEvent item)
        {
            if (item != null)
                await unitOfWork.EventRepository.AddAsync(item);
            await unitOfWork.SaveAllAsync();
                
        }
        public async Task<IEvent> UpdateAsync(IEvent item)
        {
            if (item != null)
            {
                await unitOfWork.EventRepository.UpdateAsync(item);
                var calendars = await unitOfWork.CalendarRepository.ListAsync(x => x.Events.Contains(item));

                var calendar = calendars.First();
                var index = calendar.Events.FindIndex(x => x.Id == item.Id);
                if(index != -1)
                    calendar.Events[index] = item;

                await unitOfWork.CalendarRepository.UpdateAsync(calendar);
                await unitOfWork.SaveAllAsync();
            }
            return await Task.FromResult(item);
        }

        public async Task AddEventToCalendar(IEvent item, int calendarId) 
        {
            if (item != null)
            {
                var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
                if (calendar == null) return;
                calendar.Events.Add(item);

                await unitOfWork.CalendarRepository.UpdateAsync(calendar);
                await unitOfWork.EventRepository.AddAsync(item);
                await unitOfWork.SaveAllAsync();
            }
        }

        public async Task DeleteEventFromCalendar(IEvent item, int calendarId)
        {
            if(item != null)
            {
                var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
                if (calendar == null) return;
                calendar.Events.Remove(item);

                await unitOfWork.CalendarRepository.UpdateAsync(calendar);
                await unitOfWork.EventRepository.DeleteAsync(item.Id);
                await unitOfWork.SaveAllAsync();
            }
        }

        public async Task<IEnumerable<IEvent>> GetByUser(int id)
        {
            var events = await unitOfWork.EventRepository.ListAsync(x => x.AuthorId.Equals(id));
            if (events is not null) return events;
            return null;
        }

        public async Task SendNotificationForNewEvent(int userId, IEvent @event)
        {
            //throw NotImplementedException();
        }
    }
}