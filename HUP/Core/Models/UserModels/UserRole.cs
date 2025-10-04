using HUP.Core.Models.ServiceModels;

namespace HUP.Core.Models.UserModels
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int AssignedBy { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
