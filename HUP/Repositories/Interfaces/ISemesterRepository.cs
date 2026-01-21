using HUP.Core.Entities.Academics;

namespace HUP.Repositories.Interfaces
{
    public interface ISemesterRepository : IGenericRepository<Semester>
    {
        Task<Semester> GetActiveSemesterAsync();
        Task<Semester> GetSemesterByNameAsync(string semesterName);
        Task<IEnumerable<Semester>> GetUpcomingSemestersAsync();
        Task<bool> IsRegistrationOpenAsync(Guid semesterId);
    }
}
