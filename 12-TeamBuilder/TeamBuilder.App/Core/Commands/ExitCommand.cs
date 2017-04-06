namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Utilites;

    class ExitCommand
    {
        // Exit
        public string Execute(string[] inputArgs)
        {
            // Validate arguments count
            Check.CheckLength(0, inputArgs);

            Environment.Exit(0);

            return "Bye!";
        }
    }
}
