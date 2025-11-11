using HUP.Core.Entities.Shared;

namespace HUP.Core.Entities.UserModels
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
