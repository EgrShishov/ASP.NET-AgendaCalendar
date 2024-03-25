
namespace AgendaCalendar.Domain.Entities
{
    public class EventParticipant : Entity
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public List<ParticipantPermissions> Permissions { get; set; } = new(); 
    }
}
