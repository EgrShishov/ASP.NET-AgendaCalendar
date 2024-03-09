using AgendaCalendar.Application.Abstractions;
using AgendaCalendar.Domain.Entities;
using AgendaCalendar.Persistence.Repository;

namespace AgendaCalendar.Application.Services
{
    public class CalendarService : IService<Calendar>
    {
        private CalendarRepository calendarsRepository;

        public CalendarService(CalendarRepository calendarsRepository) 
        {
            this.calendarsRepository = calendarsRepository;
        }

        /// <summary>
        /// Imports calendar with given calendar_id. Inputs csv or ics format
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        // throuh serialization lib
        public void ImportCalendar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exports calendar with given calendar_id in csv or ics format
        /// </summary>
        /// <param name="calendarId"></param>
        /// <returns></returns>
        public void ExportCalendar(int calendarId)
        {
            var myCalendar = calendarsRepository.GetByIdAsync(calendarId);
        }

        public async Task<IEnumerable<Calendar>> GetAllAsync()
        {
            return await calendarsRepository.GetListAsync();
        }
        public async Task<Calendar> GetByIdAsync(int id)
        {
            return await calendarsRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await calendarsRepository.DeleteAsync(id);
        }

        public async Task AddAsync(Calendar item)
        {
            await calendarsRepository.AddAsync(item);
        }

        public Task<Calendar> UpdateAsync(Calendar item)
        {
            throw new NotImplementedException();
        }
    }
}