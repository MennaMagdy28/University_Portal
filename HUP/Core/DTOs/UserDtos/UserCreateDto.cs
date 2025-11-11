using HUP.Core.Enums;

namespace HUP.Core.DTOs.UserDtos
{
    public class UserCreateDto
    {
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public RoleType Role { get; set; }
    }
}
