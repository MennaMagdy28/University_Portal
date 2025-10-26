namespace HUP.Core.Entities.Identity
{
    public class User : BaseEntity
    {
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<UserPagePermission> UserPagePermissions { get; set; } = new List<UserPagePermission>();
    }
}