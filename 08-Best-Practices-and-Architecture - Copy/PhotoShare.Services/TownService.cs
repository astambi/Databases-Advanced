namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System.Linq;

    public class TownService
    {
        public void Add(string townName, string countryName)
        {
            Town town = new Town()
            {
                Name = townName,
                Country = countryName
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Towns.Add(town);
                context.SaveChanges();
            }
        }

        public bool IsExisting(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.Any(t => t.Name == townName);
            }
        }

        public Town GetTownByName(string townName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Towns.SingleOrDefault(t => t.Name == townName);
            }
        }
    }
}
