using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;

namespace AgendaCalendar.Application.Authentication.Queries
{
    public sealed record AuthenticationThroughGoogleQuery() : IRequest<User> { }

    public class AuthenticationThroughGoodleQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AuthenticationThroughGoogleQuery, User>
    {
        public async Task<User> Handle(AuthenticationThroughGoogleQuery request, CancellationToken cancellationToken)
        {

            /*UserCredential credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                },
                new[] {"profile", "email"},
                "user",
                CancellationToken.None
            );

            var oauthService = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = credentials,
                ApplicationName = "AgendaCalendarClient"
            });

            var userInfo = await oauthService.Userinfo.Get().ExecuteAsync();
            Console.WriteLine(userInfo.Name);
            Console.WriteLine(userInfo.Email);
            return null;*/
            return null;
        }
    }
}
