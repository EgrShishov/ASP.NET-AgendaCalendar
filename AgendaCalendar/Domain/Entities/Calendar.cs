using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class Calendar : Entity
    {
        public Calendar() { }

        public Calendar(int authorId, List<IEvent> events, List<Reminder> reminders)
        {
            AuthorId=authorId;
            Events=events;
            Reminders=reminders;
        }

        public int AuthorId { get; set; }

        public List<IEvent> Events { get; set; } = new();

        public List<Reminder> Reminders { get; set; } = new();

        public bool Update(Calendar newCalendar)
        {
            if (newCalendar == null) return false;

            AuthorId = newCalendar.AuthorId;
            Events = newCalendar.Events;
            Reminders = newCalendar.Reminders;

            return true;
        }
    }
}