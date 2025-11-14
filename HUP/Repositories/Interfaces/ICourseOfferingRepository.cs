using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    // Repository interface for managing CourseOffering entities
    // Extends the generic repository interface for basic CRUD operations
    // Defines additional queries specific to CourseOffering
    public interface ICourseOfferingRepository : IGenericRepository<CourseOffering>
    {
        Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid DepartmentId, Guid SemesterId);
        // Retrieves course offerings that are available for the student to register in a specific department and semester
        Task<IEnumerable<CourseOffering>> GetAvailbleToRegisterAsync(Guid DepartmentId, Guid SemesterId);

    }
}
