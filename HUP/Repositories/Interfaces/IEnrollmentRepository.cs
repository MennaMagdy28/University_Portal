using HUP.Core.Entities.Academics;
namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> GetbyStudentId(Guid studentId);
    }
}
