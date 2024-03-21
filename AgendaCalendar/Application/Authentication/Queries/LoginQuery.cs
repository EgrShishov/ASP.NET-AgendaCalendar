using AgendaCalendar.Domain.Abstractions;
using AgendaCalendar.Domain.Entities;
using MediatR;

namespace AgendaCalendar.Core.Application
{
    public sealed record LoginQuery(string userName, string password) : IRequest<User> { }

    public class LoginQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<LoginQuery, User>
    {
        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.UserRepository.GetListAsync(cancellationToken);
            foreach(var user in users) 
            {
                if (user.UserName == request.userName && user.Password == request.password) return user;
            }
            return null;
        }
    }
}
