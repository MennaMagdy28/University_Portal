using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ICourseOfferingRepository : IGenericRepository<CourseOffering>
    {
        Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid DepartmentId, Guid SemesterId);
        Task<IEnumerable<CourseOffering>> GetAvailbleToRegisterAsync(Guid DepartmentId, Guid SemesterId);

    }
}
