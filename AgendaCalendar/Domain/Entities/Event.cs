using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class Event : IEvent
    {
        public Event() { }

        public Event(int id, string Title, DateTime StartTime, DateTime EndTime, string Desc, int author)
        {
            Id = id;
            this.Title = Title;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            Description = Desc;
            AuthorId = author;
        }
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<EventParticipant> EventParticipants { get; set; } = new();
        public int Id { get; set; }

        public bool Update(IEvent newEvent)
        {
            if (newEvent == null) return false;

            Title = newEvent.Title;
            StartTime = newEvent.StartTime;
            EndTime = newEvent.EndTime;
            Description = newEvent.Description;
            AuthorId = newEvent.AuthorId;
            EventParticipants = newEvent.EventParticipants;

            return true;
        }

        public override string ToString()
        {
            return $"Event : {Title}, starts : {StartTime}, Desc: {Description}";
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