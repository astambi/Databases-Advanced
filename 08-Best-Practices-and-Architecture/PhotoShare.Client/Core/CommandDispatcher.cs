namespace PhotoShare.Client.Core
{
    using Commands;
    using Services;
    using System;
    using System.Linq;
    using Utilities;

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
            PictureService pictureService = new PictureService();

            switch (commandName)
            {
                // 1. Photo Share System
                case "RegisterUser":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 4);
                    RegisterUserCommand registerUser = new RegisterUserCommand(userService);
                    result = registerUser.Execute(commandParameters);
                    break;
                case "AddTown":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    AddTownCommand addTown = new AddTownCommand(townService);
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    ModifyUserCommand modifyUser = new ModifyUserCommand(userService, townService);
                    result = modifyUser.Execute(commandParameters);
                    break;
                case "DeleteUser":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    DeleteUserCommand deleteUser = new DeleteUserCommand(userService);
                    result = deleteUser.Execute(commandParameters);
                    break;
                case "AddTag":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    AddTagCommand addTag = new AddTagCommand(tagService);
                    result = addTag.Execute(commandParameters);
                    break;
                case "CreateAlbum":
                    ValidateInput.CheckMinInputArgsCount(commandName, commandParameters.Count(), 4);
                    CreateAlbumCommand createAlbum = new CreateAlbumCommand(userService, albumService, tagService);
                    result = createAlbum.Execute(commandParameters);
                    break;
                case "AddTagTo":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    AddTagToCommand addTagTo = new AddTagToCommand(albumService, tagService);
                    result = addTagTo.Execute(commandParameters);
                    break;
                case "MakeFriends":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    MakeFriendsCommand makeFriends = new MakeFriendsCommand(userService);
                    result = makeFriends.Execute(commandParameters);
                    break;
                case "ListFriends":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 1);
                    ListFriendsCommand listFriends = new ListFriendsCommand(userService);
                    result = listFriends.Execute(commandParameters);
                    break;
                case "ShareAlbum":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    ShareAlbumCommand shareAlbum = new ShareAlbumCommand(userService, albumService);
                    result = shareAlbum.Execute(commandParameters);
                    break;
                case "UploadPicture":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 3);
                    UploadPictureCommand uploadPicture = new UploadPictureCommand(albumService, pictureService);
                    result = uploadPicture.Execute(commandParameters);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    result = exit.Execute();
                    break;

                // 2. Extend Photo Share System
                case "Login":
                    ValidateInput.CheckExactInputArgsCount(commandName, commandParameters.Count(), 2);
                    LoginCommand login = new LoginCommand(userService);
                    result = login.Execute(commandParameters);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute();
                    break;

                // 1. Photo Share System
                default:
                    throw new InvalidOperationException($"Command <{commandName}> not valid!");
            }

            return result;
        }
    }
}
