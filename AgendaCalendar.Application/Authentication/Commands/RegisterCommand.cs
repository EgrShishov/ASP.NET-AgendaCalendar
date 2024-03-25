using AgendaCalendar.Application.Common.Interfaces;

namespace AgendaCalendar.Application
{
    public sealed record RegisterCommand(string userName, string password, string email) : IRequest<User> { }

    public class RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RegisterCommand, User>
    {
        public async Task<User> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.UserRepository.ListAsync(
                user => user.UserName == request.userName && user.Password == request.password);
            if (users is not null) return null;

            var newUser = new User() 
            {
                UserName = request.userName, 
                Password = request.password, 
                Email = request.email 
            };
            await unitOfWork.UserRepository.AddAsync(newUser);
            await unitOfWork.SaveAllAsync();

            //var token = jwtTokenGenerator.GenerateToken(newUser.Id, newUser.UserName, newUser.Password);
            //return token with result
            return newUser;
        }
    }
}
