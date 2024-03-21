using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Queries
{
    public sealed record EventByIdQuery(int eventId) : IRequest<IEvent> { }

    public class EventByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<EventByIdQuery, IEvent>
    {
        public async Task<IEvent> Handle(EventByIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(request.eventId);
            if (@event != null) return null;
            return @event;
        }
    }
}
