using HUP.Core.Enums;

namespace HUP.Application.DTOs.UserDtos
{
    public class UserCreateDto
    {
        public string NationalID { get; set; }
        public string UniversityEmail { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public RoleType Role { get; set; }
    }
}
