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

            if (!this.albumService.IsExistingAlbum(albumName) ||
                !this.tagService.IsExistingTag(tagName))
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            this.albumService.AddTagToAlbum(albumName, tagName);

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
