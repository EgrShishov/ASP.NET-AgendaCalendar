using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Calendars.Commands
{
    public sealed record UpdateCalendarCommand(int calendarId, Calendar newCalendar) : IRequest<Calendar> { }

    public class UpdateCalendarCommandHandlet(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCalendarCommand, Calendar>
    {
        public async Task<Calendar> Handle(UpdateCalendarCommand request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            if (calendar == null) return null;
            calendar = request.newCalendar;
            await unitOfWork.CalendarRepository.UpdateAsync(calendar);
            await unitOfWork.SaveAllAsync();
            return calendar;
        }
    }
}
