using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        public Task<Schedule> GetByCourseOfferingIdAndGroupAsync(Guid courseOfferingId, string group);
    }
}
