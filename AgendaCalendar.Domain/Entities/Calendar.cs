using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class Calendar : Entity
    {
        public Calendar() { }

        public Calendar(string title, string description, int authorId, List<IEvent> events, List<Reminder> reminders)
        {
            Title = title;
            CalendarDescription = description;
            AuthorId = authorId;
            Events = events;
            Reminders = reminders;
        }

        public int AuthorId { get; set; }

        public string Title { get; set; }

        public string CalendarDescription { get; set; }

        public List<IEvent> Events { get; set; } = new();

        public List<Reminder> Reminders { get; set; } = new();

        public List<int> Subscribers { get; set; } = new();

        public bool Update(Calendar newCalendar)
        {
            if (newCalendar == null) return false;

            AuthorId = newCalendar.AuthorId;
            Events = newCalendar.Events;
            Reminders = newCalendar.Reminders;
            Title = newCalendar.Title;
            CalendarDescription = newCalendar.CalendarDescription;
            Subscribers = newCalendar.Subscribers;

            return true;
        }
    }
}