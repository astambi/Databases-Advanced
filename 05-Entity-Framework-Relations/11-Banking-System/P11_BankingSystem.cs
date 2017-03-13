namespace _11_Banking_System
{
    using System;
    using Core;

    class P11_BankingSystem
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Database [BankingSystem.CodeFirst]");
            BankingSystemContext context = new BankingSystemContext();
            context.Database.Initialize(true);

            Console.WriteLine("\nSolutions to Problems:\n" +
                "11. *Bank System\n" +
                "12. *Bank System Console Client: \n"+
                "     using class specific methods for bank account operations,\n" + 
                "     author input validation & command processor incorporated\n" +
                "\nRollback migrations to see previous versions of the database.\n");

            CommandProcessor(context);
        }

        private static void CommandProcessor(BankingSystemContext context)
        {
            Console.WriteLine("*************************************************************");
            Console.WriteLine("Solution to Problem 12:");
            Console.WriteLine("Enter command & parameters or type [exit]:");

            CommandProcessor cmdProcessor = new CommandProcessor();
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    string output = cmdProcessor.Execute(input);
                    Console.WriteLine(output);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }        
    }
}
