using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Application.Users.Commands
{
    public sealed record AddUserCommand(string userName, string password, string email) : IRequest<User> { }

    public class AddUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddUserCommand, User>
    {
        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                UserName = request.userName, 
                Password = request.password, 
                Email = request.email 
            };
            await unitOfWork.UserRepository.AddAsync(newUser);
            await unitOfWork.SaveAllAsync();
            return newUser;
        }
    }
}
