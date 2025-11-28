using HUP.Core.Entities.Shared;
using HUP.Core.Entities.Academics;


namespace HUP.Core.Entities.Identity
{
    public class User : BaseEntity
    {
        public string NationalId { get; set; }
        public string? Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime PasswordExpiryDate { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid RoleId { get; set; }
        public Role UserRole { get; set; }

        public UserPersonalInfo PersonalInfo { get; set; }
        public UserContact ContactInfo { get; set; }
        public ICollection<Role> CreatedRoles { get; set; } = new List<Role>();

    }
}