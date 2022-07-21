namespace ems_backend.Entities
{
    public class WishList
    {
        public int WishListId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public virtual User? User { get; set; }
        public virtual Event? Event { get; set; }
    }
}