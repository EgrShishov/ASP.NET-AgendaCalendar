
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
        bool Update(IEvent newEvent);
    }
}