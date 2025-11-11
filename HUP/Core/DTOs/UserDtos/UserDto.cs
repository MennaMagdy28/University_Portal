namespace HUP.Core.DTOs.UserDtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string NationalId { get; set; }
        public string UniversityEmail { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
