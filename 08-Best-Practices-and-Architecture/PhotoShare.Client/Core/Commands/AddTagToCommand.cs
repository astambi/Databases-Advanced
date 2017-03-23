namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;
    using Utilities;

    public class AddTagToCommand
    {
        private readonly AlbumService albumService;
        private readonly TagService tagService;

        public AddTagToCommand(AlbumService albumService, TagService tagService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string tagName = data[1].ValidateOrTransform(); // validated

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (!this.albumService.IsExistingAlbum(albumName) ||
                !this.tagService.IsExistingTag(tagName))
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            // 2. Extend Photo Share System
            if (!this.albumService.IsAlbumOwner(albumName))
            {
                throw new InvalidOperationException("Invalid credentials! You can add tags to an album only if you are the album owner.");
            }

            if (this.albumService.HasAlbumTag(albumName, tagName))
            {
                throw new InvalidOperationException($"Tag {tagName} already added to album {albumName}!");
            }

            this.albumService.AddTagToAlbum(albumName, tagName);

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
