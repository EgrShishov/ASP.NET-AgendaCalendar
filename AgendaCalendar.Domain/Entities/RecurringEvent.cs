using AgendaCalendar.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace AgendaCalendar.Domain.Entities
{
    public class RecurringEvent : Entity, IEvent
    {
        public RecurringEvent() { }

        public RecurringEvent(string Title, DateTime StartTime, DateTime EndTime, string Desc, int author, int interval, RecurrenceType type)
        {
            this.Title = Title;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            Description = Desc;
            AuthorId = author;
            Interval = interval;
            Type = type;
        }
        public int Interval {  get; set; }
        public RecurrenceType Type { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public List<EventParticipant> EventParticipants { get; set; } = new();

        public bool Update(IEvent newEvent)
        {
            var _event = newEvent as RecurringEvent;
            if (_event == null) return false;

            Title = _event.Title;
            StartTime = _event.StartTime;
            EndTime = _event.EndTime;
            Description = _event.Description;
            AuthorId = _event.AuthorId;
            Interval = _event.Interval;
            Type = _event.Type;

            return true;
        }

        public override string ToString()
        {
            return $"Event : {Title}, starts : {StartTime}, Desc: {Description}, Interval : {Interval}, Type: {Type}";
        }

        public void AddParticipant(EventParticipant participant)
        {
            if (participant != null) EventParticipants.Add(participant);
        }

        public void RemoveParticipant(EventParticipant participant)
        {
            if (participant != null) EventParticipants.Remove(participant);
        }
    }
}