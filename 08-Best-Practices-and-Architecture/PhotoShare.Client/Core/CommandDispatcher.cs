namespace PhotoShare.Client.Core
{
    using Commands;
    using Services;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string commandName = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray(); // skipping commandName

            string result = string.Empty;

            UserService userService = new UserService();
            TownService townService = new TownService();
            TagService tagService = new TagService();
            AlbumService albumService = new AlbumService();

            switch (commandName)
            {
                case "RegisterUser":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 4);
                    RegisterUserCommand registerUser = new RegisterUserCommand(userService);
                    result = registerUser.Execute(commandParameters);
                    break;
                case "AddTown":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    AddTownCommand addTown = new AddTownCommand(townService);
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    ModifyUserCommand modifyUser = new ModifyUserCommand(userService, townService);
                    result = modifyUser.Execute(commandParameters);
                    break;
                case "DeleteUser":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    DeleteUserCommand deleteUser = new DeleteUserCommand(userService);
                    result = deleteUser.Execute(commandParameters);
                    break;
                case "AddTag":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    AddTagCommand addTag = new AddTagCommand(tagService);
                    result = addTag.Execute(commandParameters);
                    break;
                case "CreateAlbum":
                    CheckMinInputArgsCount(commandName, commandParameters.Count(), 4);
                    CreateAlbumCommand createAlbum = new CreateAlbumCommand(userService, albumService, tagService);
                    result = createAlbum.Execute(commandParameters);
                    break;                    
                case "AddTagTo":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    AddTagToCommand addTagTo = new AddTagToCommand(albumService, tagService);
                    result = addTagTo.Execute(commandParameters);
                    break;






                case "MakeFriends":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    MakeFriendsCommand makeFriends = new MakeFriendsCommand();
                    result = makeFriends.Execute(commandParameters);
                    // todo
                    break;
                case "ListFriends":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    PrintFriendsListCommand listFriends = new PrintFriendsListCommand();
                    result = listFriends.Execute(commandParameters);
                    // todo
                    break;
                case "ShareAlbum":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    ShareAlbumCommand shareAlbum = new ShareAlbumCommand();
                    //result = shareAlbum.Execute(commandParameters);
                    // todo
                    break;
                case "UploadPicture":
                    CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    UploadPictureCommand uploadPicture = new UploadPictureCommand();
                    result = uploadPicture.Execute(commandParameters);
                    // todo
                    break;



                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute();
                    break;
                default:
                    throw new InvalidOperationException($"Command {commandName} not valid!");
            }

            return result;
        }

        private static void CheckExactInputArgsCount(string commandName, int argsCount, int requiredCount)
        {
            if (argsCount != requiredCount)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }
        }

        private static void CheckMinInputArgsCount(string commandName, int argsCount, int requriedMinCount)
        {
            if (argsCount < requriedMinCount)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }
        }
    }
}
