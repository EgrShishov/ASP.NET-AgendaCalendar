using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Commands
{
    public sealed record UpdateEventCommand(IEvent @event) : IRequest<IEvent> { }

    public class UpdateEventCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateEventCommand, IEvent>
    {
        public async Task<IEvent> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.EventRepository.UpdateAsync(request.@event);
            var calendars = await unitOfWork.CalendarRepository.ListAsync(x => x.Events.Contains(request.@event));

            var calendar = calendars.First();
            var index = calendar.Events.FindIndex(x => x.Id == request.@event.Id);
            if (index != -1)
                calendar.Events[index] = request.@event;

            await unitOfWork.CalendarRepository.UpdateAsync(calendar);
            await unitOfWork.SaveAllAsync();
            return request.@event;
        }
    }
}
