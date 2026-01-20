using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repository;

        public InstructorService(IInstructorRepository repository)
        {
            _repository = repository;
        }

        public async Task<Instructor> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Instructor>> GetByDepartmentAsync(Guid departmentId)
        {
            return await _repository.GetByDepartmentAsync(departmentId);
        }

        public async Task<IEnumerable<Course>> GetAssignedCoursesAsync(Guid instructorId, Guid semesterId)
        {
            return await _repository.GetAssignedCoursesAsync(instructorId, semesterId);
        }

        public async Task<IEnumerable<ClassGroup>> GetScheduleAsync(Guid instructorId, Guid semesterId)
        {
            return await _repository.GetInstructorScheduleAsync(instructorId, semesterId);
        }
    }
}
