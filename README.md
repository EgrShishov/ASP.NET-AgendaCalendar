# Shishov Egor, 253505
# AgendaCalendar
### AgendaCalendar - is a simple service, that allows users to create and edit events. Users can enable a recurring functionality with optional parametrs for frequency.
# Functionality
   1. Import and export calendar data in common formats like iCal or CSV, or in my custom format(need to think).
   2. Adding reminders and sending them through email(SmtpClient in .NET), that specifed user.
   3. Create recurring events.
   4. Edit existing events to update information like title, time, description, etc.
   5. Create events by specifying title, description, start time, end time, and other relevant details
   6. Delete existing events from the calendar.
# System's class diagram
![изображение](https://github.com/EgrShishov/ASP.NET-AgendaCalendar/assets/112828095/68ad197f-8051-4927-ac88-933e9acebdf4)
## Data Access Layer 
 ### Events
1. GetEvents - return all events stored in database.
2. GetUserEvents -return all user's events.
3. GetEvent - return one event by event_id.
4. CreateEvent - creates event and adds it into database.
5. UpdateEvent - updates event.
6. DeleteEvent - removes event from database.
7. CreateRecurringEvent - creates recurring event, user can choose frequency from RepetitivityType enum or create your own type.
8. AddReminder - adds reminder to event.
 ### User
1. AddUser - insert user in db.
2. GetUserById - returns user with requested id.
3. DeleteUser - deletes user from db with requested id.
4. Auth - user authorisation.
 ### Reminder
1. CreateReminder - creates a remainder and inserts in db.
2. EditReminder - changes reminder's fields.
3. GetAllReminders - returns all reminders stores in repository.
4. GetReminderById - return one reminder by unique id.
5. GetUpcomingRemainders - returns reminders within n days.
6. RemoveReminder - removes reminder from repository.
 ### Calendar
1. ExportCalendar - exports calendar in specific format through serialization lib.
2. ImportCalendar - deserialize calendar and uploads it.
# Description of data models
1. User:
 - unique userId for searching in db
 - UserName (is unique for each user) it's what users typically use to log in.
 - Password for ensuring the security of user accounts
 - Email - it used to send reminding notifications
2. Event:
 - unique EventId for searching in db
 - Title - provides a quick overview of what the event is about and is typically displayed prominently in the calendar view.
 - Description - provides more information about event, or any other relevant information that users might need to know. 
 - StartTime - indicates when the event begins.
 - EndTime - indicates when the event ends.
3. Recurring event:
 - Contains the same fields as Event, but thanks to interface we have
 - Interval - period.
 - Type - daily, weekly, monthly, yearly
4. Reminder:
 - ReminderTime - the specific time when the reminder is scheduled to trigger. 
 - Description - provides information about what the user needs to remember.
5. Calendar:
 - Id 
 - AuthorId
 - Events - list of events, specific to that calendar
 - Reminders - list of reminders of this calendar
