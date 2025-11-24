using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Repositories.Interfaces;
using System.Threading.Tasks;
using HUP.Application.DTOs.AcademicDtos;
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
            var enrollment = EnrollmentMapper.ToEntityFromCreateDto(dto);
            enrollment.Id = Guid.NewGuid();
            enrollment.EnrollmentDate = DateTime.Now;
            enrollment.CreatedAt = DateTime.Now;
            enrollment.Status = EnrollmentStatus.Registered;
            await _repository.AddAsync(enrollment);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnrollmentResponseDto>> GetAllAsync()
        {
            var entities =  await _repository.GetAllAsync();
            var dtos = entities.Select(e => EnrollmentMapper.ToResponseDto(e));
            return dtos;
        }

        public async Task<EnrollmentResponseDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = EnrollmentMapper.ToResponseDto(entity);
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

        public async Task Update(EnrollmentResponseDto dto)
        {
            var entity = EnrollmentMapper.ToEntityFromResponseDto(dto);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
