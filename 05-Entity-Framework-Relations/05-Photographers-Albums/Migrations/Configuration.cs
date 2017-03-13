namespace _05_Photographers_Albums.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotographersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false; // Problem 6
        }

        protected override void Seed(PhotographersContext context)
        {
            //  This method will be called after migrating to the latest version.
            Console.WriteLine("Seeding data");

            // Seed Photographers: Problem 5
            context.Photographers.AddOrUpdate(p => p.Username,
                new Photographer()
                {
                    Username = "tom",
                    Password = "12345$%^PASSword",
                    Email = "tom@gmail.com",
                    RegisterDate = DateTime.Now,
                    BirthDate = new DateTime(1970, 8, 5)
                },
                new Photographer()
                {
                    Username = "elsa",
                    Password = "PASSword123$%^TheBest",
                    Email = "elsa@gmail.com",
                    RegisterDate = DateTime.Now,
                    BirthDate = new DateTime(1990, 11, 6)
                },
                new Photographer()
                {
                    Username = "hans",
                    Password = "ord123$%^hjhjhjh",
                    Email = "hans@gmail.com",
                    RegisterDate = DateTime.Now,
                    BirthDate = null
                });
            context.SaveChanges();

            // Seed Pictures: Problem 6
            context.Pictures.AddOrUpdate(p => p.Title,
                new Picture()
                {
                    Title = "Lava",
                    Caption = "Photographing Flowing Lava in Hawaii",
                    Path = "https://visualwilderness.com/wp-content/uploads/2013/01/hawaii17_00423-3-copy.jpg"
                },
                new Picture()
                {
                    Title = "Arizona",
                    Caption = "Coyote Buttes, Arizona",
                    Path = "https://visualwilderness.com/wp-content/uploads/2016/01/Sunset-Swirls.jpg"
                },
                new Picture()
                {
                    Title = "Wind",
                    Caption = "Capturing the Wind",
                    Path = "http://pushfurther.indurogear.com/wp-content/uploads/2016/12/iceland_DSCODSCO2736-copy.jpg"
                });
            context.SaveChanges();

            // Seed Albums: Problem 6
            context.Albums.AddOrUpdate(a => a.Name,
                new Album()
                {
                    Name = "Visual Wilderness",
                    BackgroundColor = "Solid Black",
                    //PhotographerId = 1, // Disable for Problem 9
                    IsPublic = true
                },
                new Album()
                {
                    Name = "Bautiful Landscapes",
                    BackgroundColor = "Transparent Blue",
                    //PhotographerId = 2, // Disable for Problem 9
                    IsPublic = true
                },
                new Album()
                {
                    Name = "Nature",
                    BackgroundColor = "No Color",
                    //PhotographerId = 3, // Disable for Problem 9
                    IsPublic = false
                });
            context.SaveChanges();

            // Seed AlbumsPhotographers: Add for Problem 9.Shared Albums, Remove for Problem 10.Roles
            //context.Albums
            //    .FirstOrDefault(c => c.Name.Contains("Nature"))
            //    .Photographers.Add(context.Photographers.Find(1));
            //context.Albums
            //    .FirstOrDefault(c => c.Name.Contains("Nature"))
            //    .Photographers.Add(context.Photographers.Find(2));
            //context.Albums
            //    .FirstOrDefault(c => c.Name.Contains("Nature"))
            //    .Photographers.Add(context.Photographers.Find(3));
            //context.Albums
            //    .FirstOrDefault(c => c.Name.Contains("Landscapes"))
            //    .Photographers.Add(context.Photographers.Find(2));
            //context.Albums
            //    .FirstOrDefault(c => c.Name.Contains("Wilderness"))
            //    .Photographers.Add(context.Photographers.Find(3));    
            //context.SaveChanges();

            // Seed AlbumsPhotographers with Roles: Problem 10
            context.PhotographerAlbums.AddOrUpdate(a => new { a.Photographer_Id, a.Album_Id },
                new PhotographerAlbum()
                {
                    Photographer_Id = 1,
                    Album_Id = 3,
                    Role = Role.Owner
                },
                new PhotographerAlbum()
                {
                    Photographer_Id = 2,
                    Album_Id = 2,
                    Role = Role.Owner
                },
                new PhotographerAlbum()
                {
                    Photographer_Id = 2,
                    Album_Id = 3,
                    Role = Role.Viewer
                },
                new PhotographerAlbum()
                {
                    Photographer_Id = 3,
                    Album_Id = 1,
                    Role = Role.Owner
                },
                new PhotographerAlbum()
                {
                    Photographer_Id = 3,
                    Album_Id = 3,
                    Role = Role.Viewer
                });
            context.SaveChanges();

            // Seed PicturesAlbums: Problem 6
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Nature"))
                .Pictures.Add(context.Pictures.Find(1));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Nature"))
                .Pictures.Add(context.Pictures.Find(2));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Nature"))
                .Pictures.Add(context.Pictures.Find(3));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Landscapes"))
                .Pictures.Add(context.Pictures.Find(1));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Landscapes"))
                .Pictures.Add(context.Pictures.Find(2));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Landscapes"))
                .Pictures.Add(context.Pictures.Find(3));
            context.Albums
                .FirstOrDefault(c => c.Name.Contains("Wilderness"))
                .Pictures.Add(context.Pictures.Find(1));
            context.SaveChanges();

            // Seed Tags: Problem 7
            context.Tags.AddOrUpdate(t => t.Name,
                new Tag() { Name = "#Nature" },
                new Tag() { Name = "#Sunsets" },
                new Tag() { Name = "#Landscapes" },
                new Tag() { Name = "#Favourites" });
            context.SaveChanges();

            // Seed AlbumsTags: Problem 7
            context.Albums
                .Where(a => a.Name.Contains("Nature")).ToList()
                .ForEach(a => a.Tags.Add(context.Tags.Find("#Nature")));
            context.Albums
                .Where(a => a.Name.Contains("Nature")).ToList()
                .ForEach(a => a.Tags.Add(context.Tags.Find("#Favourites")));
            context.Albums
                .Where(a => a.Name.Contains("Landscapes")).ToList()
                .ForEach(a => a.Tags.Add(context.Tags.Find("#Landscapes")));
            context.Albums
                .Where(a => a.Name.Contains("Landscapes")).ToList()
                .ForEach(a => a.Tags.Add(context.Tags.Find("#Nature")));
            context.Albums
                .Where(a => a.Name.Contains("Wilderness")).ToList()
                .ForEach(a => a.Tags.Add(context.Tags.Find("#Sunsets")));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
