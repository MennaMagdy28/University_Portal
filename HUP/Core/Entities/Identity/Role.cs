namespace HUP.Core.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ? Description { get; set; }
        public Guid CreatedBy { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public User User { get; set; }
}
