namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System.Linq;

    public class TagService
    {
        public void AddTag(string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Tags.Add(new Tag() { Name = tagName });
                context.SaveChanges();
            }
        }
        
        // AddTagToAlbum => In AlbumService        

        public bool IsExistingTag(string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Tags.Any(t => t.Name == tagName);
            }
        }
    }
}
