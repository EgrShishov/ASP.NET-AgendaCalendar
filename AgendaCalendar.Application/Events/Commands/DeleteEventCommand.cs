using AgendaCalendar.Domain.Abstractions;
using MediatR;

namespace AgendaCalendar.Application.Events.Commands
{
    public sealed record DeleteEventCommand(int calednarId, IEvent @event) : IRequest<IEvent> { }

    public class DeleteEventCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteEventCommand, IEvent>
    {
        public async Task<IEvent> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calednarId);
            if (calendar == null) return null;
            calendar.Events.Remove(request.@event);

            await unitOfWork.CalendarRepository.UpdateAsync(calendar);
            await unitOfWork.EventRepository.DeleteAsync(request.@event.Id);
            await unitOfWork.SaveAllAsync();
            return request.@event;
        }
    }
}
