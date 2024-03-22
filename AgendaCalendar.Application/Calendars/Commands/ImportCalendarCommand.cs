using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Calendars.Commands
{
    public sealed record ImportCalendarCommand() : IRequest<Calendar> { }

    public class ImportCalendarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ImportCalendarCommand, Calendar>
    {
        public Task<Calendar> Handle(ImportCalendarCommand request, CancellationToken cancellationToken)
        {
            //var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            //if (calendar == null) return string.Empty;
            return null;
        }
    }
}
