namespace HUP.Core.Entities.Identity
{
    public class UserContact
    {
        public Guid UserID { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string AltEmail { get; set; }

        public User User { get; set; }
    }
}