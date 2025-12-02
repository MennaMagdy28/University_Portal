using HUP.Core.Entities.Academics;
using HUP.Core.Models;

namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetByStudentId(Guid studentId);
        Task<IEnumerable<Enrollment>> GetbySemester(Semester semester, Guid studentId);
        Task<Enrollment?> GetExistingAsync(Guid studentId, Guid courseId);
        Task<IEnumerable<Enrollment>> GetByStudentAndSemesterAsync(Guid studentId, string semester);
        Task<List<SemesterGrades>> GetStudentSemesterGradeModelsAsync(Guid studentId);
        Task<IEnumerable<Enrollment>> GetRegisteredByStudentAsync(Guid studentId);
    }
}
