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
            string[] tags = data.Skip(3).ToArray();

            if (!this.userService.IsExistingUser(username))
            {
                throw new ArgumentException($"User {username} not found!");
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

            if (tags.Any(t => !this.tagService.IsExistingTag(TagUtilities.ValidateOrTransform(t))))
            {
                throw new ArgumentException("Invalid tags!");
            }

            string[] validTags = tags.Select(t => TagUtilities.ValidateOrTransform(t)).ToArray();

            this.albumService.AddAlbum(username, albumTitle, color, validTags);

            return $"Album {albumTitle} successfully created!";
        }
    }
}
