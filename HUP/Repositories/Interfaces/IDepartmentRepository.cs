using HUP.Core.Entities.Academics;
namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetByFacultyIdAsync(Guid facultyId);
    }
}
