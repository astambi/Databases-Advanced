namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System.Linq;

    public class PictureService
    {
        public bool IsExistingPicture(string pictureTitle, string pictureFilePath)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Pictures.Any(p => p.Title == pictureTitle && p.Path == pictureFilePath);
            }
        }

        public void AddPictureWithouAlbum(string pictureTitle, string pictureFilePath)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Picture picture = new Picture()
                {
                    Title = pictureTitle,
                    Path = pictureFilePath
                };

                context.Pictures.Add(picture);
                context.SaveChanges();
            }
        }
    }
}
