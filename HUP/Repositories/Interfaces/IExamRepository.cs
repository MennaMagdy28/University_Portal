using HUP.Core.Entities.Academics;
namespace HUP.Repositories.Interfaces
{
    // Extends the generic repository interface for basic CRUD operations
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Task<IEnumerable<Exam>> GetByCoursesAsync(List<Guid> courseIds);

        Task<IEnumerable<Exam>> GetAllActiveAsync();
        Task UpdateAsync(Exam exam);
        Task DeleteAsync(Guid id);
    }
}
