namespace _05_Photographers_Albums
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotographersContext : DbContext
    {
        public PhotographersContext()
            : base("name=PhotographersContext")
        {
            Database.SetInitializer<PhotographersContext>(new MigrateDatabaseToLatestVersion<PhotographersContext, Configuration>());
        }

        public virtual DbSet<Photographer> Photographers { get; set; }
        public virtual DbSet<Album> Albums { get; set; }    // Problem 6
        public virtual DbSet<Picture> Pictures { get; set; } // Problem 6
        public virtual DbSet<Tag> Tags { get; set; }        // Problem 7 
        public virtual DbSet<PhotographerAlbum> PhotographerAlbums { get; set; } // Problem 10
    }
}