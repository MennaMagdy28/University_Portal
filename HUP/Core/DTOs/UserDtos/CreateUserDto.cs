namespace HUP.Core.DTOs.UserDtos
{
    public class CreateUserDto
    {
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
