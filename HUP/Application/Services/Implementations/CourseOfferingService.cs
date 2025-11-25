using HUP.Application.Mappers;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;
using HUP.Application.Services.Interfaces;
using System.Threading.Tasks;
using HUP.Application.DTOs.AcademicDtos.CourseOffering;


namespace HUP.Application.Services.Implementations
{
    public class CourseOfferingService : ICourseOfferingService
    {
        private readonly ICourseOfferingRepository _repository;

        public CourseOfferingService(ICourseOfferingRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseOfferingDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return null;
            
            return CourseOfferingMapper.ToDto(entity);
        }

        public async Task<IEnumerable<CourseOfferingDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return CourseOfferingMapper.ToDto(entities);
        }

        public async Task<bool> Exists(CreateCourseOfferingDto dto)
        {
            var entity = _repository.GetExistingAsync(dto.CourseId, dto.DepartmentId, dto.SemesterId);
            return entity != null;
        }

        public async Task AddAsync(CreateCourseOfferingDto dto)
        {
            var entity = CourseOfferingMapper.ToEntity(dto);
            entity.Id = Guid.NewGuid();
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        // public async Task Update(Guid id, CreateCourseOfferingDto dto)
        // {
        //     // Need to be implemented
        //     await _repository.SaveChangesAsync();
        // }

        public async Task SoftDelete(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.Now;
            await _repository.SaveChangesAsync();
        }

        public async Task Remove(Guid id)
        {
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
        }


        public async Task<IEnumerable<CourseOfferingDto>> GetActiveCourseOfferingAsync(Guid departmentId, Guid semesterId)
        {
            var entities = await _repository.GetActiveCourseOfferingAsync(departmentId, semesterId);
            return CourseOfferingMapper.ToDto(entities);
        }

        public async Task<IEnumerable<CourseOfferingDto>> GetAvailableToRegisterAsync(Guid studentId)
        {
            var courses = await _repository.GetAvailableToRegisterAsync(studentId);
            var dtos = CourseOfferingMapper.ToDto(courses);
            return dtos;
        }
    }
}

