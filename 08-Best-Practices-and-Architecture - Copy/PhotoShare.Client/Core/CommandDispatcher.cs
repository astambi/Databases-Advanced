namespace PhotoShare.Client.Core
{
    using Commands;
    using Services;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            //throw new NotImplementedException();

            string commandName = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray(); // skipping commandName

            string result = string.Empty;

            TownService townService = new TownService();
            UserService userService = new UserService();

            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand(userService);
                    result = registerUser.Execute(commandParameters);
                    break;
                case "AddTown":
                    AddTownCommand addTown = new AddTownCommand(townService);
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    ModifyUserCommand modifyUser = new ModifyUserCommand(userService, townService);
                    result = modifyUser.Execute(commandParameters);
                    break;


                // TODO

                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute();
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
