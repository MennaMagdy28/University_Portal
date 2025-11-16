using HUP.Application.Mappers;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Services.Implementations
{
    public class CourseOfferingService : ICourseOfferingService
    {
        private readonly ICourseOfferingRepository _repository;
        private readonly CourseOfferingMapper _mapper;

        public CourseOfferingService(ICourseOfferingRepository repository, CourseOfferingMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CourseOfferingDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            
            return _mapper.ToDto(entity);
        }

        public async Task<IEnumerable<CourseOfferingDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.ToDto(entities);
        }

        public async Task AddAsync(CourseOffering entity)
        {
            entity.Id = Guid.NewGuid();
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

        public async Task<IEnumerable<CourseOfferingDto>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId)
        {
            var entities = await _repository.GetActiveCourseOfferingAsync(departmentId, semesterId);
            return _mapper.ToDto(entities);
        }

        public async Task<IEnumerable<CourseOfferingDto>> GetAvailableToRegisterAsync(Guid studentId)
        {
            return await _repository.GetAvailbleToRegisterAsync(studentId);
        }
    }
}

