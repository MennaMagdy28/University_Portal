using HUP.Core.Models.Shared;

namespace HUP.Core.Models.UserModels
{
    public class UserRole : BaseEntity
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public int AssignedBy { get; set; }
        public DateOnly ExpiryDate { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
