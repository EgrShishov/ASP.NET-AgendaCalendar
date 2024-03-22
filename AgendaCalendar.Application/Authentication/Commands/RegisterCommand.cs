using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Core.Application
{
    public sealed record RegisterCommand(string userName, string password, string email) : IRequest<User> { }

    public class RegisterCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterCommand, User>
    {
        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.UserRepository.GetListAsync();
            foreach(var user in users) 
            {
                if (user.UserName == request.userName && user.Password == request.password) return null;
            }

            var newUser = new User(request.userName, request.password, request.email);
            await unitOfWork.UserRepository.AddAsync(newUser);
            await unitOfWork.SaveAllAsync();
            return newUser;
        }
    }
}
