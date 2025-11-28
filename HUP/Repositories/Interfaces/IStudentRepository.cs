using HUP.Core.Entities.Academics;
using HUP.Core.Enums;

namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    // and adds updating academic status and CGPA, and retrieving students by faculty or department
    public interface IStudentRepository :  IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetByFacultyAsync(Guid facultyId);
        Task<IEnumerable<Student>> GetByDepartmentAsync(Guid departmentId);

        Task UpdateAsync(Student student);
        Task UpdateAcademicStatusAsync(Guid studentId, AcademicStatus status);
    }
}
