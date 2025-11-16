using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;
using HUP.Services.Interfaces;

namespace HUP.Services.Implementations
{
    public class CourseOfferingService : ICourseOfferingService
    {
        private readonly ICourseOfferingRepository _repository;

        public CourseOfferingService(ICourseOfferingRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseOffering> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CourseOffering>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddAsync(CourseOffering entity)
        {
            await _repository.AddAsync(entity);
        }

        public void Update(CourseOffering entity)
        {
            _repository.Update(entity);
        }

        public void SoftDelete(Guid id)
        {
            _repository.SoftDelete(id);
        }

        public void Remove(Guid id)
        {
            _repository.Remove(id);
        }

        public async Task SaveChangesAsync()
        {
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseOffering>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId)
        {
            return await _repository.GetActiveCourseOfferingAsync(departmentId, semesterId);
        }

        public async Task<IEnumerable<CourseOffering>> GetAvailableToRegisterAsync(Guid studentId)
        {
            return await _repository.GetAvailbleToRegisterAsync(studentId);
        }
    }
}

