namespace TeamBuilder.App.Core.Commands
{
    using Data;
    using Models;
    using System;
    using System.Globalization;
    using Utilites;

    class CreateEventCommand
    {
        // CreateEvent <name> <description> <startDate> <endDate>
        // Date format [dd/MM/yyyy HH:mm] => NB! date split in two arguments
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(6, inputArgs); // NB. 6 arguments as dateTime comes as 2 arguments [date time]

            // Authorize user
            AuthenticationManager.Authorize();

            // Get arguments
            string eventName = inputArgs[0];
            string description = inputArgs[1];
            DateTime startDateTime, endDateTime;

            bool isValidStartDateTime = DateTime.TryParseExact(
                inputArgs[2] + " " + inputArgs[3],  // [date time]
                Constants.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDateTime);

            bool isValidEndDateTime = DateTime.TryParseExact(
                inputArgs[4] + " " + inputArgs[5],  // [date time]
                Constants.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out endDateTime);

            // Validate date format
            if (!isValidStartDateTime || !isValidEndDateTime)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            // Validate startDate < endDate
            if (startDateTime >= endDateTime)
            {
                throw new ArgumentException(Constants.ErrorMessages.StartDateBeforeEndDate);
            }

            // Create event
            CreateEvent(eventName, description, startDateTime, endDateTime);

            return $"Event {eventName} was created successfully!";
        }

        private static void CreateEvent(string eventName, string description, DateTime startDateTime, DateTime endDateTime)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                Event newEvent = new Event()
                {
                    Name = eventName,
                    Description = description,
                    StartDate = startDateTime,
                    EndDate = endDateTime,
                    CreatorId = AuthenticationManager.GetCurrentUser().Id // current user
                };

                context.Events.Add(newEvent);
                context.SaveChanges();
            }
        }
    }
}
