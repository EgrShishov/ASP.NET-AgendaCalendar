using AgendaCalendar.Persistence.Data;
using AgendaCalendar.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaCalendar.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, UnitOfWork>()
                    .AddSingleton<AppDbContext>(new AppDbContext())
                    .AddSingleton<ReminderRepository>()
                    .AddSingleton<UsersRepository>()
                    .AddSingleton<EventsRepository>()
                    .AddSingleton<CalendarRepository>();
            return services;
        }
    }
}
