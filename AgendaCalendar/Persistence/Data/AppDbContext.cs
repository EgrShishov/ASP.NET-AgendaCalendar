using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using System.Text.Json;

namespace AgendaCalendar.Persistence.Data
{
    public class AppDbContext
    {
        private FileStream? CalendarDbContext;
        private FileStream? UserDbContext;
        private FileStream? ReminderDbContext;
        private FileStream? EventDbContext;

        public void SaveChanges()
        {
            CalendarDbContext.SetLength(0);
            UserDbContext.SetLength(0);
            ReminderDbContext.SetLength(0);
            EventDbContext.SetLength(0);

            var calendarJson = JsonSerializer.Serialize<List<Calendar>>(Calendars,
                new JsonSerializerOptions { WriteIndented = true});
            var userJson = JsonSerializer.Serialize<List<User>>(Users,
                 new JsonSerializerOptions { WriteIndented = true });
            var eventJson = JsonSerializer.Serialize<List<IEvent>>(Events,
                 new JsonSerializerOptions { WriteIndented = true });
            var reminderJson = JsonSerializer.Serialize<List<Reminder>>(Reminders,
                 new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter sw = new(CalendarDbContext))
            {
                sw.WriteLine(calendarJson);
            }
            using (StreamWriter sw = new(UserDbContext))
            {
                sw.WriteLine(userJson);
            }
            using (StreamWriter sw = new(ReminderDbContext))
            {
                sw.WriteLine(reminderJson);
            }
            using (StreamWriter sw = new(EventDbContext))
            {
                sw.WriteLine(eventJson);
            }
        }

        public void DeleteDatabase()
        {
            CalendarDbContext.Close();
            UserDbContext.Close();
            EventDbContext.Close();
            ReminderDbContext.Close();
        }

        public void CreateDatabase()
        {
            CalendarDbContext = new("CalendarTable.txt", FileMode.OpenOrCreate);
            UserDbContext = new("UserTable.txt", FileMode.OpenOrCreate);
            EventDbContext = new("EventTable.txt", FileMode.OpenOrCreate);
            ReminderDbContext = new("ReminderTable.txt", FileMode.OpenOrCreate);

            Calendars = JsonSerializer.Deserialize<List<Calendar>>(CalendarDbContext);
            Users = JsonSerializer.Deserialize<List<User>>(UserDbContext);
            //Events = JsonSerializer.Deserialize<List<IEvent>>(EventDbContext);
            Reminders = JsonSerializer.Deserialize<List<Reminder>>(ReminderDbContext);
        }

        public List<Calendar> Calendars { get; set; } = new();
        public List<IEvent> Events { get; set; } = new();
        public List<Reminder> Reminders { get; set; } = new();
        public List<User> Users { get; set; } = new();
    }
}