using HUP.Core.Enums;

namespace HUP.Core.DTOs.UserDtos
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string NationalID { get; set; }
        public string UniversityEmail { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public RoleType Role { get; set; }
        public bool IsActive { get; set; }
    }
}
