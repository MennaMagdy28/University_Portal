namespace HUP.Core.Models;

public class UserSummary
{
    public Guid Id;
    public string NationalId { get; set; }
    public string FullName { get; set; }
    public string RoleName { get; set; }
}