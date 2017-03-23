namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Services;
    using System;

    public class ShareAlbumCommand
    {
        private readonly UserService userService;
        private readonly AlbumService albumService;

        public ShareAlbumCommand(UserService userService, AlbumService albumService)
        {
            this.userService = userService;
            this.albumService = albumService;
        }

        // ShareAlbum <albumId> <username> <permission>        
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            Role role;
            bool isValidRole = Enum.TryParse(permission, out role);

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (!this.albumService.IsExistingAlbum(albumId))
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            // 2. Extend Photo Share System
            if (!this.albumService.IsAlbumOwner(albumId))
            {
                throw new InvalidOperationException("Invalid credentials! You can share your own albums only.");
            }

            if (!isValidRole)
            {
                throw new AggregateException($"Permission must be either \"Owner\" or \"Viewer\"!");
            }

            Album album = this.albumService.GetAlbumById(albumId);

            if (albumService.IsAlbumSharedWithUserInRole(albumId, username, role))
            {
                throw new InvalidOperationException($"Album {album.Name} already shared with user {username} ({role.ToString()})!");
            }

            this.albumService.ShareAlbum(albumId, username, role);

            return $"Username {username} added to album {album.Name} ({permission})";
        }
    }
}
