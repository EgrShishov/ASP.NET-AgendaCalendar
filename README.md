# Shishov Egor, 253505
# AgendaCalendar
### AgendaCalendar - is a simple service, that allows users to create and edit events. Users can enable a recurring functionality with optional parametrs for frequency.
# Functionality
   1. Import and export calendar data in common formats like iCal or CSV, or in my custom format(need to think).
   2. Sending reminders the day before event through email(SmtpClient in .NET), that specifed user.
   3. Create recurring events and custom recurrence rules such as every other week, every third Monday, etc.
   4. Edit existing events to update information like title, time, location, description, etc.
   5. Create events by specifying title, description, location, start time, end time, and other relevant details
   6. Delete existing events from the calendar.
# System's class diagram
pic of class diagram here
## Data Access Layer 
 ### Events
1. GetEvents - return all events stored in database.
2. GetUserEvents -return all user's events.
3. GetEvent - return one event by event_id.
4. CreateEvent - creates event and adds it into database.
5. UpdateEvent - updates event.
6. DeleteEvent - removes event from database.
7. CreateRecurringEvent - creates recurring event, user can choose frequency from RepetitivityType enum or create your own type.
 ### User
1. AddUser - insert user in db.
2. GetUserById - returns user with requested id.
3. DeleteUser - deletes user from db with requested id.
4. Auth - user authorisation.
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
 - IsRecurring - indicates whether the event is a one-time occurrence or if it repeats at regular intervals
3. Recurring event:
4. Reminder:
 - ReminderTime - the specific time when the reminder is scheduled to trigger. 
 - Description - provides information about what the user needs to remember. 
