using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;
using System.Threading.Tasks;

using HUP.Core.Enums;
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
            dto.Status = EnrollmentStatus.Registered;
            dto.Id = Guid.NewGuid();
            dto.EnrollmentDate = DateTime.UtcNow;
            var enrollment = EnrollmentMapper.ToEntity(dto);
            await _repository.AddAsync(enrollment);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CreateEnrollmentDto>> GetAllAsync()
        {
            var entities =  await _repository.GetAllAsync();
            var dtos = entities.Select(e => EnrollmentMapper.ToDto(e));
            return dtos;
        }

        public async Task<CreateEnrollmentDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = EnrollmentMapper.ToDto(entity);
            return dto;
        }

        public async Task Remove(Guid id)
        {
            _repository.Remove(id);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDelete(Guid id)
        {
            _repository.SoftDelete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task Update(CreateEnrollmentDto dto)
        {
            var entity = EnrollmentMapper.ToEntity(dto);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
