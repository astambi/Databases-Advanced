namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System;
    using System.Linq;

    public class AlbumService
    {
        public Tag TagUtilities { get; private set; }

        public void AddAlbum(string username, string albumName, Color color, string[] validTagNames)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = new Album()
                {
                    Name = albumName,
                    BackgroundColor = color,
                    Tags = context.Tags.Where(t => validTagNames.Contains(t.Name)).ToList()
                };
                User owner = context.Users.SingleOrDefault(u => u.Username == username);
                if (owner != null)
                {
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
        }

        public void AddTagToAlbum(string albumName, string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                try
                {
                    context.Tags
                        .FirstOrDefault(t => t.Name == tagName)
                        .Albums
                        .Add(context.Albums.FirstOrDefault(a => a.Name == albumName));
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException($"Tag {tagName} already added to album {albumName}!");
                }
            }
        }

        public bool IsExistingAlbum(string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Any(a => a.Name == albumName);
            }
        }
    }
}
