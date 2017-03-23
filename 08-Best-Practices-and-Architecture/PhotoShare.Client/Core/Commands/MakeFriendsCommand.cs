namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class MakeFriendsCommand
    {
        private readonly UserService userService;

        public MakeFriendsCommand(UserService userService)
        {
            this.userService = userService;
        }

        // MakeFriends <username1> <username2>
        public string Execute(string[] data)
        {
            string username1 = data[0];
            string username2 = data[1];

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (!this.userService.IsExistingUser(username1))
            {
                throw new ArgumentException($"User {username1} not found!");
            }
            else if (!this.userService.IsExistingUser(username2))
            {
                throw new ArgumentException($"User {username2} not found!");
            }

            // 2. Extend Photo Share System
            if (!userService.HasProfileRights(username1))
            {
                throw new InvalidOperationException("Invalid credentials! You can add friends only to your own profile.");
            }

            if (this.userService.IsFriendToUser(username1, username2) == true) // user2 is friend to user1
            {
                throw new InvalidOperationException($"{username2} is already a friend to {username1}");
            }

            this.userService.MakeFriends(username1, username2);

            return $"Friend {username2} added to {username1}!";
        }
    }
}
