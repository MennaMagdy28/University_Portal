namespace HUP.Core.Entities.Identity
{
    public class UserRole : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int AssignedBy { get; set; }
        public DateTime AssignedAt { get; set; }
        public DateTime ExpiryDate { get; set; }
        
    }
}