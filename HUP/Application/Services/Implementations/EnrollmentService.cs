using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;
        public EnrollmentService(IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(CreateEnrollmentDto dto)
        {
            var enrollment = EnrollmentMapper.ToEntity(dto);
            await _repository.AddAsync(enrollment);
        }

        public Task AddAsync(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Enrollment> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
