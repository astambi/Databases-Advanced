namespace PhotoShare.Services
{
    using Data;
    using Models;
    using System.Linq;

    public class TownService
    {
        public void AddTown(string townName, string countryName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Town town = new Town()
                {
                    Name = townName,
                    Country = countryName
                };

                context.Towns.Add(town);
                context.SaveChanges();
            }
        }

        public bool IsExistingTown(string townName)
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
