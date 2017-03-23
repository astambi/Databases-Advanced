namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class AlbumService
    {
        public void AddAlbum(string username, string albumName, Color color, string[] tagNames)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = new Album()
                {
                    Name = albumName,
                    BackgroundColor = color,
                    Tags = context.Tags.Where(t => tagNames.Contains(t.Name)).ToList()
                };
                User owner = context.Users.SingleOrDefault(u => u.Username == username);

                AlbumRole albumRole = new AlbumRole()
                {
                    User = owner,
                    Album = album,
                    Role = Role.Owner
                };
                context.AlbumRoles.Add(albumRole);  // v.1
                //album.AlbumRoles.Add(albumRole);  // v.2
                //context.Albums.Add(album);        // v.2

                context.SaveChanges();
            }
        }

        public bool HasAlbumTag(string albumName, string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums
                    .Include("Tags")
                    .SingleOrDefault(a => a.Name == albumName)
                    .Tags
                    .Any(t => t.Name == tagName);
            }
        }

        public void AddTagToAlbum(string albumName, string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag tag = context.Tags.SingleOrDefault(t => t.Name == tagName);
                Album album = context.Albums.SingleOrDefault(a => a.Name == albumName);

                album.Tags.Add(tag);
                context.SaveChanges();
            }
        }

        public bool IsExistingAlbum(string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Any(a => a.Name == albumName);
            }
        }

        public bool IsExistingAlbum(int albumId)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Any(a => a.Id == albumId);
            }
        }

        public Album GetAlbumById(int albumId)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.SingleOrDefault(a => a.Id == albumId);
            }
        }

        public bool IsAlbumSharedWithUserInRole(int albumId, string username, Role role)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.AlbumRoles
                    .Any(ar =>
                            ar.Album.Id == albumId &&
                            ar.User.Username == username &&
                            ar.Role == role);
            }
        }

        public void ShareAlbum(int albumId, string username, Role role)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                AlbumRole albumRole = new AlbumRole()
                {
                    Album = context.Albums.SingleOrDefault(a => a.Id == albumId),
                    User = context.Users.SingleOrDefault(u => u.Username == username),
                    Role = role
                };

                context.AlbumRoles.Add(albumRole);
                context.SaveChanges();
            }
        }

        public void AddPictureToAlbum(string albumName, string pictureTitle, string pictureFilePath)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .SingleOrDefault(a => a.Name == albumName);
                Picture picture = context.Pictures
                    .SingleOrDefault(p => p.Title == pictureTitle && p.Path == pictureFilePath);

                album.Pictures.Add(picture);
                context.SaveChanges();
            }
        }

        public bool IsPictureInAlbum(string albumName, string pictureTitle, string pictureFilePath)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums
                    .Include("Pictures")
                    .SingleOrDefault(a => a.Name == albumName)
                    .Pictures
                    .Any(p => p.Title == pictureTitle && p.Path == pictureFilePath);
            }
        }

        public bool IsAlbumOwner(string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                int currentUserId = AuthenticationService.GetCurrentUser().Id;
                return context.AlbumRoles
                    .Any(ar =>
                            ar.Album.Name == albumName &&
                            ar.User.Id == currentUserId &&
                            ar.Role == Role.Owner);
            }
        }

        public bool IsAlbumOwner(int albumId)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                int currentUserId = AuthenticationService.GetCurrentUser().Id;
                return context.AlbumRoles
                    .Any(ar =>
                            ar.Album.Id == albumId &&
                            ar.User.Id == currentUserId &&
                            ar.Role == Role.Owner);
            }
        }
    }
}
