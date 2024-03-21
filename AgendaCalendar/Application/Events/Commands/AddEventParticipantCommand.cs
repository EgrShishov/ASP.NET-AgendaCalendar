using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Events.Commands
{
    public sealed record AddEventParticipantCommand(int eventId, EventParticipant eventParticipant) : IRequest<IEvent> { }

    public class AddEventParticipantCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddEventParticipantCommand, IEvent>
    {
        public async Task<IEvent> Handle(AddEventParticipantCommand request, CancellationToken cancellationToken)
        {
            var @event = await unitOfWork.EventRepository.GetByIdAsync(request.eventId);
            if (@event == null) return null;
            
            @event.AddParticipant(request.eventParticipant);
            await unitOfWork.EventRepository.UpdateAsync(@event);
            await unitOfWork.SaveAllAsync();
            return @event;
        }
    }
}
