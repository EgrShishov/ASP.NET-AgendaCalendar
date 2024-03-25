using _De_SerializationLib;

namespace AgendaCalendar.Application.Calendars.Commands
{
    public sealed record ImportCalendarCommand(string ical_format, int author_id) : IRequest<Calendar> { }

    public class ImportCalendarCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ImportCalendarCommand, Calendar>
    {
        public async Task<Calendar> Handle(ImportCalendarCommand request, CancellationToken cancellationToken)
        {
            Calendar calendar = IcalConverter.Deserialize(request.ical_format);
            if (calendar == null) return null;
            calendar.AuthorId = request.author_id;
            await unitOfWork.CalendarRepository.AddAsync(calendar);
            await unitOfWork.SaveAllAsync();

            return calendar;
        }
    }
}
