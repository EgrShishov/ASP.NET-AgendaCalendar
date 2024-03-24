
namespace AgendaCalendar.Application.Calendars.Commands
{
    public sealed record ExportCalendarCommand(int calendarId) : IRequest<string> { }

    public class ExportCalendarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ExportCalendarCommand, string>
    {
        public async Task<string> Handle(ExportCalendarCommand request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calendarId);
            if (calendar == null) return string.Empty;
            //through serialization library implement
            return string.Empty;
        }
    }
}
