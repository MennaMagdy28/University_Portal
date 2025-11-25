using HUP.Core.Entities.Academics;
namespace HUP.Repositories.Interfaces
{
    // Repository interface for managing ProgramPlan entities
    // Defines methods for adding, retrieving, updating, and removing ProgramPlan records
    // Each ProgramPlan is uniquely identified by a combination of DepartmentId and CourseId
    // This interface does not extend a generic repository interface to allow for custom method signatures specific to ProgramPlan management
    public interface IProgramPlanRepository
    {
        Task AddAsync(ProgramPlan entity);
        Task<IEnumerable<ProgramPlan>> GetAllAsync();
        Task<IEnumerable<ProgramPlan>> GetByDepartmentAsync(Guid departmentId);
        Task<ProgramPlan> GetByIdAsync(Guid deptId, Guid courseId);
        Task RemoveAsync(Guid deptId, Guid courseId);
        Task SaveChangesAsync();
        void Update(ProgramPlan entity);
    }
}
