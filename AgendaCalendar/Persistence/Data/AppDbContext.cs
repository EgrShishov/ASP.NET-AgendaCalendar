﻿using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using System.Text.Json;
using Newtonsoft.Json;

namespace AgendaCalendar.Persistence.Data
{
    public class AppDbContext
    {
        private FileStream? CalendarDbContext;
        private FileStream? UserDbContext;
        private FileStream? ReminderDbContext;
        private FileStream? EventDbContext;

        public AppDbContext() 
        {
            CreateDatabase();
        }

        public Task SaveChanges()
        {
            UserDbContext = new("UserTable.txt", FileMode.OpenOrCreate);
            ReminderDbContext = new("ReminderTable.txt", FileMode.OpenOrCreate);

            UserDbContext.SetLength(0);
            ReminderDbContext.SetLength(0);

            var calendarJson = JsonConvert.SerializeObject(Calendars, Newtonsoft.Json.Formatting.Indented,
            new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            var userJson = System.Text.Json.JsonSerializer.Serialize<List<User>>(Users,
                 new JsonSerializerOptions { WriteIndented = true });
            var eventJson = JsonConvert.SerializeObject(Events, Newtonsoft.Json.Formatting.Indented,
            new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            var reminderJson = System.Text.Json.JsonSerializer.Serialize<List<Reminder>>(Reminders,
                 new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter sw = new(UserDbContext))
            {
                sw.WriteLine(userJson);
            }
            using (StreamWriter sw = new(ReminderDbContext))
            {
                sw.WriteLine(reminderJson);
            }
            File.WriteAllText("CalendarTable.txt", calendarJson);
            File.WriteAllText("EventTable.txt",eventJson);
            return Task.CompletedTask;
        }

        public Task DeleteDatabase()
        {
            Calendars.Clear();
            Events.Clear();
            Users.Clear();
            Reminders.Clear();
            return Task.CompletedTask;
        }

        public Task CreateDatabase()
        {
            Calendars = new();
            Reminders = new();
            Events = new();
            Users = new();
            //CalendarDbContext = new("CalendarTable.txt", FileMode.OpenOrCreate);
            UserDbContext = new("UserTable.txt", FileMode.OpenOrCreate);
            ReminderDbContext = new("ReminderTable.txt", FileMode.OpenOrCreate);

            Calendars = JsonConvert.DeserializeObject<List<Calendar>>(File.ReadAllText("CalendarTable.txt"),
                new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            Users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(UserDbContext);
            Events = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IEvent>>(File.ReadAllText("EventTable.txt"),
                new Newtonsoft.Json.JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            Reminders = System.Text.Json.JsonSerializer.Deserialize<List<Reminder>>(ReminderDbContext);

            //CalendarDbContext.Close();
            UserDbContext.Close();
            ReminderDbContext.Close();
            return Task.CompletedTask;
        }

        public List<Calendar>? Calendars { get; set; }
        public List<IEvent>? Events { get; set; }
        public List<Reminder>? Reminders { get; set; }
        public List<User>? Users { get; set; }
    }
}