namespace ProductsShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserFriend
    {
        [Key, Column(Order = 1)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Key, Column(Order = 2)]
        public int FriendId { get; set; }

        public virtual User Friend { get; set; }
    }
}
