namespace HUP.Core.DTOs.UserDtos
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string NationalID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
