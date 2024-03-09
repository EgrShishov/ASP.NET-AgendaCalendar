using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class RecurringEvent : Entity, IEvent
    {
        public RecurringEvent() { }

        public RecurringEvent(int id, string Title, DateTime StartTime, DateTime EndTime, string Desc, int author, int interval, RecurrenceType type)
        {
            Id = id;
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

        public bool Update(IEvent newEvent)
        {
            if (newEvent == null) return false;

            Title = newEvent.Title;
            StartTime = newEvent.StartTime;
            EndTime = newEvent.EndTime;
            Description = newEvent.Description;
            AuthorId = newEvent.AuthorId;
            //Interval = newEvent.Interval;
            //Type = newEvent.Type;

            return true;
        }

        public override string ToString()
        {
            return $"Event : {Title}, starts : {StartTime}, Desc: {Description}, Interval : {Interval}";
        }
    }
}