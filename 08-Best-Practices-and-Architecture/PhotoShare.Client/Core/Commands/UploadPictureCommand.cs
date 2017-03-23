namespace PhotoShare.Client.Core.Commands
{
    using Services;
    using System;

    public class UploadPictureCommand
    {
        private readonly AlbumService albumService;
        private readonly PictureService pictureService;

        public UploadPictureCommand(AlbumService albumService, PictureService pictureService)
        {
            this.albumService = albumService;
            this.pictureService = pictureService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string pictureTitle = data[1];
            string pictureFilePath = data[2];

            // 2. Extend Photo Share System
            if (!AuthenticationService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials! You should log in first.");
            }

            if (!this.albumService.IsExistingAlbum(albumName))
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            if (!this.pictureService.IsExistingPicture(pictureTitle, pictureFilePath))
            {
                this.pictureService.AddPictureWithouAlbum(pictureTitle, pictureFilePath);
            }

            // 2. Extend Photo Share System
            if (!this.albumService.IsAlbumOwner(albumName))
            {
                throw new InvalidOperationException("Invalid credentials! You can upload pictures to an album only if you are the album owner.");
            }

            if (this.albumService.IsPictureInAlbum(albumName, pictureTitle, pictureFilePath))
            {
                throw new InvalidOperationException($"Picture {pictureTitle} ({pictureFilePath}) is already added to album {albumName}!");
            }

            this.albumService.AddPictureToAlbum(albumName, pictureTitle, pictureFilePath); // attach to album

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
