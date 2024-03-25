
namespace AgendaCalendar.Domain.Entities
{
    public class RecurrenceRule //analog of reccurency pattern in ical.net
    {
        public RecurrenceFrequency Frequency { get; set; }

        /// <summary>
        /// Specifies how often the recurrence should repeat.
        /// 1-based index (e.g., 1st, 2nd, 3rd, 4th, -1 for last)
        /// </summary>
        public int Interval { get; set; }
        public RecurrenceDayOfWeek DayOfWeek { get; set; }
        public List<int> DaysOfMonth { get; set; }
        public List<int> WeeksOfMonth { get; set; } // For example, first, second, third, fourth, or last week of the month
        public List<int> MonthsOfYear { get; set; } // For example, January, February, March, etc.
        public int Year { get; set; } // Yearly recurrence
        public List<DateTime> RecurrenceDates { get; set; }
    }
}
