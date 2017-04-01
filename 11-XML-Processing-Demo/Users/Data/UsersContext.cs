namespace Users
{
    using System.Data.Entity;

    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("name=UsersContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(uf =>
                {
                    uf.MapLeftKey("UserId");
                    uf.MapRightKey("FriendId");
                    uf.ToTable("UserFriends");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}