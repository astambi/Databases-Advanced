namespace TeamBuilder.App.Core
{
    using System;

    public class Engine
    {
        private CommandDispatcher commandDispatcher;
        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        public void Run()
        {
            Console.WriteLine("Enter commands: ");

            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = this.commandDispatcher.Dispatch(input);

                    Console.WriteLine("   " + output); // for readability
                }
                catch (Exception e)
                {
                    Console.WriteLine("   " + e.GetBaseException().Message); // for readability
                }
            }
        }
    }
}
