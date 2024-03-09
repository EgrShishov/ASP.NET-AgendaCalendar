
using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class Reminder : Entity
    {
        public Reminder() { }
        public Reminder(string desc, DateTime time, string emailAdress) 
        {
            Description = desc;
            ReminderTime = time;
            Email = emailAdress;
        }
        public string Description { get; set; }

        public DateTime ReminderTime { get; set; }

        public string Email {  get; set; }

        public bool Update(Reminder newReminder)
        {
            if (newReminder == null) return false;

            Description = newReminder.Description;
            ReminderTime = newReminder.ReminderTime;

            return true;
        }
    }
}