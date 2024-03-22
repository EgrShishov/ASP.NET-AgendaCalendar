﻿using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Queries
{
    public sealed record EventListByDateRangeQuery(int calendarId, DateTime startDate, DateTime endDate) : IRequest<IEnumerable<IEvent>> { }

    public class EventListByDateRangeQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<EventListByDateRangeQuery, IEnumerable<IEvent>>
    {
        public async Task<IEnumerable<IEvent>> Handle(EventListByDateRangeQuery request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            if (calendar is not null)
                return calendar.Events.Where(x => x.StartTime.Date >= request.startDate.Date && x.EndTime.Date <= request.endDate.Date);
            return null;
        }
    }
}
