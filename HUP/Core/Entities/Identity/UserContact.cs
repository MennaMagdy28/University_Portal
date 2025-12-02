namespace HUP.Core.Entities.Identity
{
    public class UserContact
    {
        public Guid UserId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AltEmail { get; set; }

    }
}