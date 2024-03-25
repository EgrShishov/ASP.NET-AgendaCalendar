﻿using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Reminders.Commands
{
    public sealed record AddReminderCommand(string desc, DateTime time, string emailAdress, int eventId) : IRequest<Reminder> { }

    public class AddReminderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddReminderCommand, Reminder>
    {
        public async Task<Reminder> Handle(AddReminderCommand request, CancellationToken cancellationToken)
        {
            var reminder = new Reminder()
            {
                Description = request.desc, 
                ReminderTime = request.time, 
                Email = request.emailAdress, 
                EventId = request.eventId
            };
            await unitOfWork.ReminderRepository.AddAsync(reminder);
            await unitOfWork.SaveAllAsync();

            return reminder;
        }
    }
}
