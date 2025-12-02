namespace HUP.Application.DTOs.IdentityDtos.UserDtos
{
    public class UserDto
    {
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public Guid RoleId { get; set; }
    }
}