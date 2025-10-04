namespace HUP.Core.DTOs.ServiceDtos
{
    public class RoleDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<RolePagePermissionDto> Permissions { get; set; }
    }
}
