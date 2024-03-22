using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Queries
{
    public sealed record EventListByDateQuery(int calendarId, DateTime date) : IRequest<IEnumerable<IEvent>> { }

    public class EventListByDateQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<EventListByDateQuery, IEnumerable<IEvent>>
    {
        public async Task<IEnumerable<IEvent>> Handle(EventListByDateQuery request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            if (calendar is not null)
                return calendar.Events.Where(x => x.StartTime.Date == request.date.Date);
            return null;
        }
    }
}
