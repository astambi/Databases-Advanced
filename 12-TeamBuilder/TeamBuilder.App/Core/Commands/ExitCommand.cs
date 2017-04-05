namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Utilites;

    class ExitCommand
    {
        // Exit
        public string Execute(string[] args)
        {
            Check.CheckLength(0, args);
            Console.WriteLine("Bye!");

            Environment.Exit(0);

            return "Bye!";
        }
    }
}
