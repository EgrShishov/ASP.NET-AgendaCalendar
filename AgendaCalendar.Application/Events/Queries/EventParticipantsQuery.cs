
namespace AgendaCalendar.Application.Events.Queries
{
    public sealed record EventParticipantsQuery(int eventId) : IRequest<IReadOnlyList<EventParticipant>> { }

    public class EventParticipantQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<EventParticipantsQuery, IReadOnlyList<EventParticipant>>
    {
        public async Task<IReadOnlyList<EventParticipant>> Handle(EventParticipantsQuery request, CancellationToken cancellationToken)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(request.eventId);

            if (@event == null) return null;
            return @event.EventParticipants;        
        }
    }
}
