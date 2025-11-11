using HUP.Core.Entities.AcademicModels;

namespace HUP.Repositories.Interfaces
{
    public interface IProgramRepository
    {
        Task<ProgramEntity> GetByIdAsync(int id);
        Task<IEnumerable<ProgramEntity>> GetAllAsync();
        Task<IEnumerable<ProgramEntity>> GetByDepartmentIdAsync(int departmentId);
        Task AddAsync(ProgramEntity program);
        Task UpdateAsync(ProgramEntity program);
        Task DeleteAsync(int id);
    }
}
