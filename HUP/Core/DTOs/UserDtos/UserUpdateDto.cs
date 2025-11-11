namespace HUP.Core.DTOs.UserDtos
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
