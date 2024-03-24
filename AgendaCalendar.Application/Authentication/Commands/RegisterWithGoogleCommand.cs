using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;

namespace AgendaCalendar.Application.Authentication.Commands
{
    public sealed record RegisterWithGoogleCommand() : IRequest<User> { }

    public class RegisterWithGoogleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterWithGoogleCommand, User>
    {
        public async Task<User> Handle(RegisterWithGoogleCommand request, CancellationToken cancellationToken)
        {

           /* UserCredential credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                new[] { "profile", "email", "" },
                "user",
                CancellationToken.None
            );

            var oauthService = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials,
                ApplicationName = "AgendaCalendarClient"
            });

            var userInfo = await oauthService.Userinfo.Get().ExecuteAsync();
            //use tokens to authorize instead password
            var user = new User(userInfo.Name, null , userInfo.Email);
            await unitOfWork.UserRepository.AddAsync(user);*/
            return null;
        }
    }
}
