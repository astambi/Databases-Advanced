namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Services;
    using System;
    using System.Linq;
    using Utilities;

    public class CreateAlbumCommand
    {
        private readonly AlbumService albumService;
        private readonly UserService userService;
        private readonly TagService tagService;

        public CreateAlbumCommand(UserService userService, AlbumService albumService, TagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];
            string albumTitle = data[1];
            string bgColor = data[2];
            string[] tags = data.Skip(3).ToArray(); // there must be at least 1 tag

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            } 

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            // 2. Extend Photo Share System
            if (!userService.HasProfileRights(username))
            {
                throw new InvalidOperationException("Invalid credentials! You can create albums only with your own profile.");
            }

            if (this.albumService.IsExistingAlbum(albumTitle))
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }

            Color color;
            bool isValidColor = Enum.TryParse(bgColor, out color);
            if (!isValidColor)
            {
                throw new AggregateException($"Color {bgColor} not found!");
            }

            tags = tags.Select(t => TagUtilities.ValidateOrTransform(t)).ToArray(); // validate tags

            if (tags.Any(t => !this.tagService.IsExistingTag(t)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            this.albumService.AddAlbum(username, albumTitle, color, tags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
