namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;
    using System.Collections.Generic;

    public class ListFriendsCommand 
    {
        private readonly UserService userService;

        public ListFriendsCommand(UserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            List<string> friends = userService.GetUserFriends(username); // usernames only

            if (friends.Count == 0)
                return "No friends for this user.:(";

            return $"Friends:\n-{String.Join("\n-", friends)}";
        }
    }
}
