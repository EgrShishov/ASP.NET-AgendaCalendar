using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Events.Commands
{
    public sealed record AddEventCommand(int calednarId, IEvent @event): IRequest<Calendar> { }
    public class AddEventCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddEventCommand, Calendar>
    {
        public async Task<Calendar> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var calendar = await unitOfWork.CalendarRepository.GetByIdAsync(request.calednarId);
            if (calendar == null) return null;

            calendar.Events.Add(request.@event);
            await unitOfWork.CalendarRepository.UpdateAsync(calendar);
            await unitOfWork.EventRepository.AddAsync(request.@event);
            await unitOfWork.SaveAllAsync();
            return calendar;
        }
    }
}
