using AgendaCalendar.Application.Common.Interfaces;

namespace AgendaCalendar.Application
{
    public sealed record LoginQuery(string userName, string password) : IRequest<User> { }

    public class LoginQueryHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginQuery, User>
    {
        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.UserRepository.ListAsync(
                user => user.UserName == request.userName && user.Password == request.password);
            if (users is null) return null;
            var user = users.First();
            //var token = jwtTokenGenerator.GenerateToken(user.Id, user.UserName, user.Email);
            return user;
        }
    }
}
