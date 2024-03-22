using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Calendars.Queries
{
    public sealed record CalendarByIdQuery(int calendarId) : IRequest<Calendar> { }

    public class CalendarByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CalendarByIdQuery, Calendar>
    {
        public async Task<Calendar> Handle(CalendarByIdQuery request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            if (calendar != null) return calendar;
            return null;
        }
    }
}
