using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class ClassGroupService : IClassGroupService
    {
        private readonly IClassGroupRepository _repository;
        private readonly ICourseOfferingRepository _courseOfferingRepo;
        private readonly IInstructorRepository _instructorRepo;
        private readonly ILogger<ClassGroupService> _logger;

        public ClassGroupService(
            IClassGroupRepository repository,
            ICourseOfferingRepository courseOfferingRepo,
            IInstructorRepository instructorRepo,
            ILogger<ClassGroupService> logger)
        {
            _repository = repository;
            _courseOfferingRepo = courseOfferingRepo;
            _instructorRepo = instructorRepo;
            _logger = logger;
        }

        public async Task<ClassGroupDto> GetByIdAsync(Guid id)
        {
            var group = await _repository.GetByIdAsync(id);
            if (group == null)
                return null;

            return ClassGroupMapper.ToDto(group);
        }

        public async Task<IEnumerable<ClassGroupDto>> GetAllAsync()
        {
            var groups = await _repository.GetAllAsync();
            return ClassGroupMapper.ToDto(groups);
        }

        public async Task<IEnumerable<ClassGroupDto>> GetByCourseOfferingAsync(Guid courseOfferingId)
        {
            var groups = await _repository.GetByCourseOfferingAsync(courseOfferingId);
            return ClassGroupMapper.ToDto(groups);
        }

        public async Task<IEnumerable<ClassGroupDto>> GetByInstructorAsync(Guid instructorId)
        {
            var groups = await _repository.GetByInstructorAsync(instructorId);
            return ClassGroupMapper.ToDto(groups);
        }

        public async Task<ClassGroupDto> CreateAsync(CreateClassGroupDto dto)
        {
            try
            {
                var courseOffering = await _courseOfferingRepo.GetByIdAsync(dto.CourseOfferingId);
                if (courseOffering == null)
                    throw new ArgumentException("Course offering not found");

                var instructor = await _instructorRepo.GetByIdAsync(dto.InstructorId);
                if (instructor == null)
                    throw new ArgumentException("Instructor not found");

                var existingGroups = await _repository.GetByCourseOfferingAsync(dto.CourseOfferingId);
                if (existingGroups.Any(g => g.GroupCode == dto.GroupCode))
                    throw new ArgumentException($"Group with code {dto.GroupCode} already exists for this course offering");

                var group = ClassGroupMapper.ToEntity(dto);
                group.Id = Guid.NewGuid();
                group.CreatedAt = DateTime.Now;
                group.IsActive = true;
                group.CurrentEnrollment = 0;

                await _repository.AddAsync(group);
                await _repository.SaveChangesAsync();

                return await GetByIdAsync(group.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating class group");
                throw;
            }
        }

        public async Task<ClassGroupDto> UpdateAsync(Guid id, UpdateClassGroupDto dto)
        {
            try
            {
                var group = await _repository.GetByIdAsync(id);
                if (group == null)
                    throw new KeyNotFoundException("Class group not found");

                if (group.CurrentEnrollment > 0)
                {
                    if (group.DayOfWeek != dto.DayOfWeek ||
                        group.StartTime != dto.StartTime ||
                        group.EndTime != dto.EndTime)
                    {
                        throw new InvalidOperationException("Cannot change schedule of a group with enrolled students");
                    }
                }

                ClassGroupMapper.ToEntity(dto, group);
                group.UpdatedAt = DateTime.Now;

                await _repository.SaveChangesAsync();

                return await GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating class group {id}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var group = await _repository.GetByIdAsync(id);
                if (group == null)
                    return false;

                if (group.CurrentEnrollment > 0)
                    throw new InvalidOperationException("Cannot delete a group with enrolled students");

                await _repository.RemoveAsync(id);
                await _repository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting class group {id}");
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            try
            {
                var group = await _repository.GetByIdAsync(id);
                if (group == null)
                    return false;

                group.IsActive = false;
                group.UpdatedAt = DateTime.Now;

                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error soft deleting class group {id}");
                throw;
            }
        }

        public async Task<bool> IsGroupAvailableAsync(Guid groupId)
        {
            var group = await _repository.GetByIdAsync(groupId);
            if (group == null)
                return false;

            return group.IsActive && group.CurrentEnrollment < group.Capacity;
        }

        public async Task<int> GetAvailableSeatsAsync(Guid groupId)
        {
            var group = await _repository.GetByIdAsync(groupId);
            if (group == null)
                return 0;

            return Math.Max(0, group.Capacity - group.CurrentEnrollment);
        }
    }
}
