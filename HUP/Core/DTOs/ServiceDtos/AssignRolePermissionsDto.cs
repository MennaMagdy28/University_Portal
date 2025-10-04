namespace HUP.Core.DTOs.ServiceDtos
{
    public class AssignRolePermissionsDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
        public int PageId { get; set; }
    }
}
