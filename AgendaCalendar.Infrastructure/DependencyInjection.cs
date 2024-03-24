using AgendaCalendar.Application.Common.Interfaces;
using AgendaCalendar.Infrastructure.Authentication;
using AgendaCalendar.Infrastructure.Email;
using AgendaCalendar.Infrastructure.Persistence.Data;
using AgendaCalendar.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaCalendar.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>()
                    .AddSingleton<AppDbContext>()
                    .AddSingleton<ReminderRepository>()
                    .AddSingleton<UsersRepository>()
                    .AddSingleton<EventsRepository>()
                    .AddSingleton<CalendarRepository>()
                    .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
                    .AddSingleton<IEmailSender, EmailSender>();
            return services;
        }
    }
}
