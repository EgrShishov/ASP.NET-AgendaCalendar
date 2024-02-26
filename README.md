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
GetEvents - return all events stored in database.
GetUserEvents -return all user's events.
GetEvent - return one event by event_id.
CreateEvent - creates event and adds it into database.
UpdateEvent - updates event.
DeleteEvent - removes event from database.
CreateRecurringEvent - creates recurring event, user can choose frequency from RepetitivityType enum or create your own type.
# Description of data models
User - contains unique id, name and password, email.
Event - contains authorId, Description, StartDateTime, EndDateTime, Title and if IsRecurring(with type and interval) and list of participants 
Reminder - 
Notification -
/* add category to events, to show in wgich color should it shown */
