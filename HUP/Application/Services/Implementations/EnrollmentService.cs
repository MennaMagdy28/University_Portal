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
        private readonly EnrollmentMapper _mapper;
        public EnrollmentService(IEnrollmentRepository repository, EnrollmentMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task AddAsync(CreateEnrollmentDto dto)
        {
            var enrollment = _mapper.ToEntity(dto);
            return _repository.AddAsync(enrollment);
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
