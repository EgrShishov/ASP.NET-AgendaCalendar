using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Reminders.Commands
{
    public sealed record UpdateReminderCommand(int reminderId, Reminder newReminder) : IRequest<Reminder> { }

    public class EditReminderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateReminderCommand, Reminder>
    {
        public async Task<Reminder> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = await unitOfWork.ReminderRepository.GetByIdAsync(request.reminderId);
            if (reminder == null) return null;
            reminder = request.newReminder;
            await unitOfWork.ReminderRepository.UpdateAsync(reminder);
            await unitOfWork.SaveAllAsync();
            return reminder;
        }
    }
}
