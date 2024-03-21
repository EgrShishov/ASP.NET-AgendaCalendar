using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Queries
{
    public sealed record EventListQuery() : IRequest<IReadOnlyList<IEvent>> { }

    public class EventListQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<EventListQuery, IReadOnlyList<IEvent>>
    {
        public async Task<IReadOnlyList<IEvent>> Handle(EventListQuery request, CancellationToken cancellationToken)
        {
            var events = await unitOfWork.EventRepository.GetListAsync();
            if (events == null) return null;
            return events;
        }
    }
}
