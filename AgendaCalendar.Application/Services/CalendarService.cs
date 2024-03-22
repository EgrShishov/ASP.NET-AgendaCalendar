using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using Newtonsoft.Json;

namespace AgendaCalendar.Application.Services
{
    public class CalendarService : IService<Calendar>
    {
        private IUnitOfWork unitOfWork;

        public CalendarService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }

        // through serialization lib
        public void ImportCalendar(Stream calendarData)
        {
            var newCalendar = new Calendar();
            
            throw new NotImplementedException();
        }

        //through serialization lib
        public Stream ExportCalendar(int calendarId)
        {
            var myCalendar = unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
            var jsonCalendar = JsonConvert.SerializeObject(myCalendar, Formatting.Indented,
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Calendar>> GetAllAsync()
        {
            return await unitOfWork.CalendarRepository.GetListAsync();
        }
        public async Task<Calendar> GetByIdAsync(int id)
        {
            return await unitOfWork.CalendarRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await unitOfWork.CalendarRepository.DeleteAsync(id);
            await unitOfWork.SaveAllAsync();
        }

        public async Task AddAsync(Calendar item)
        {
            await unitOfWork.CalendarRepository.AddAsync(item);
            await unitOfWork.SaveAllAsync();
        }

        public async Task<Calendar> UpdateAsync(Calendar item)
        {
            var updatedCalendar = await unitOfWork.CalendarRepository.UpdateAsync(item);
            await unitOfWork.SaveAllAsync();
            return updatedCalendar;
        }

        public async Task<IEnumerable<Calendar>> GetUserCalendars(int id)
        {
            var calendars = await unitOfWork.CalendarRepository.ListAsync(x => x.AuthorId.Equals(id));
            if(calendars is not null)
            {
                return calendars;
            }
            return null;
        }

        public async Task SubscribeToCalendar(int calendarId, int userId)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
            if(calendar is not null) calendar.Subscribers.Add(userId);
        }

        public async Task UnsubscribeFromCalendar(int calendarId, int userId)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(calendarId);
            if (calendar is not null) calendar.Subscribers.Remove(userId);
        }

        public async Task<IEnumerable<int>> GetUserSubscriptions(int userId)
        {
            var userCalendars = await unitOfWork.CalendarRepository.ListAsync(x => x.Subscribers.Contains(userId));
            var userSubscriptions = new List<int>();
            if (userCalendars is not null)
            {
                foreach (var item in userCalendars)
                {
                    userSubscriptions.Add(item.Id);
                }
            }
            return userSubscriptions;
        }

        public async Task AddCalendarByUrl(string url) //integration with another ical calendars
        {
            throw new NotImplementedException();
        }

        public async Task ExportCalendarByUrl(int calendarId)
        {
            throw new NotImplementedException();
        }
    }
}