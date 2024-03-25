
namespace AgendaCalendar.Infrastructure.Email
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";
        public string Host { get; init; } = null!;
        public int Port { get; init; }
        public string ServiceEmail { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
