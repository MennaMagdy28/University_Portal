namespace HUP.Application.DTOs.IdentityDtos.UserDtos;

public class UsersListResponse
{
    public Guid Id;
    public string NationalId { get; set; }
    public string FullName { get; set; }
    public string RoleName { get; set; }

}