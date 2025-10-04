namespace HUP.Core.DTOs.CommonDtos
{
    public class DepartmentDto
    {
        public int ID { get; set; }
        public int FacultyID { get; set; }
        public string DepartmentName { get; set; }
        public string HeadOfDepartment { get; set; }

        public FacultyDto Faculty { get; set; }
    }
}
