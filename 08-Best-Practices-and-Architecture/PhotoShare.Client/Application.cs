namespace PhotoShare.Client
{
    using Core;
    using System;

    public class Application
    {
        public static void Main()
        {
            Console.WriteLine("Solutions to Best Practices and Architecture:\n" +
                "1. Photo Share System\n" +
                "2. Extend Photo Share System\n\n" + 
                "Please log in to test Solution 1. as user authentication is implemented\n\n" + 
                "Enter commands:");

            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }
    }
}
