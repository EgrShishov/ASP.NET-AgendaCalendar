
using AgendaCalendar.Domain.Entities;
using System.ComponentModel;

namespace AgendaCalendar.Domain.Abstractions
{
    public interface IEvent
    {
        int Id { get; set; }
        int AuthorId { get; set; }
        string Description { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        string Title { get; set; }
        List<EventParticipant> EventParticipants { get; set; }
        void AddParticipant(EventParticipant participant);
        void RemoveParticipant(EventParticipant participant);
        bool Update(IEvent newEvent);
    }
}