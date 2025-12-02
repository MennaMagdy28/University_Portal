using HUP.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace HUP.Core.Entities.Academics
{
    // Represents the program plan for a specific department, detailing required courses and their types
    // Each ProgramPlan entry links a department to a course along with its requirement type and whether it is compulsory
    // Composite key: DepartmentId + CourseId
    // Example: A ProgramPlan entry could specify that for the SWE department, the course "Algorithms" is an elective faculty-level requirement
    // but for the CS department, the same course might be an elective faculty-level requirement
    public class ProgramPlan 
    {
        //the courses required for a specific department's program
        public Guid DepartmentId { get; set; }
        public Guid CourseId { get; set; }
        public RequirementType RequirementType { get; set; } //requirement type: University, Faculty, Department
        // indicates if the course is compulsory or elective
        public bool IsCompulsory { get; set; }
        public decimal FinalGrade { get; set; }
        public Course Course { get; set; }
        public Department Department { get; set; }
    }
}