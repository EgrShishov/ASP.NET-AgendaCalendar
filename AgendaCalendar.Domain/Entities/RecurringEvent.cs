using AgendaCalendar.Domain.Abstractions;

namespace AgendaCalendar.Domain.Entities
{
    public class RecurringEvent : Entity, IEvent
    {
        public RecurringEvent() { }

        public int AuthorId { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public RecurrenceRule ReccurenceRules { get; set; }
        public List<EventParticipant> EventParticipants { get; set; } = new();

        public string Location { get; set; }
        public override string ToString()
        {
            return $"Event : {Title}, starts : {StartTime}, Desc: {Description}, Interval : {ReccurenceRules.Interval}, Frequency: {ReccurenceRules.Frequency}";
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