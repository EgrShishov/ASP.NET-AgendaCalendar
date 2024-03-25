
namespace AgendaCalendar.Application.Reminders.Queries
{
    public sealed record ReminderListByEventIdQuery(int eventId) : IRequest<IReadOnlyList<Reminder>> { }

    public class ReminderListByEventIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<ReminderListByEventIdQuery, IReadOnlyList<Reminder>>
    {
        public async Task<IReadOnlyList<Reminder>> Handle(ReminderListByEventIdQuery request, CancellationToken cancellationToken)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(request.eventId);
            if (@event == null) return null;
            var reminders = await unitOfWork.ReminderRepository.ListAsync(x => x.Id.Equals(request.eventId));
            if (reminders == null) return null;
            return reminders;
        }
    }
}

