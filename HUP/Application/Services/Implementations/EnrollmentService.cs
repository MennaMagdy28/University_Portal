using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Application.DTOs.AcademicDtos.Enrollment;
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

        public async Task<bool> Exists(CreateEnrollmentDto dto)
        {
            var entity = await _repository.GetExistingAsync(dto.StudentId, dto.CourseId);
            return entity != null;
        }

        public async Task Remove(Guid id)
        {
            await _repository.RemoveAsync(id);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDelete(Guid id)
        {
            var enrollment = await _repository.GetByIdAsync(id);
            enrollment.IsDeleted = true;
            enrollment.UpdatedAt = DateTime.Now;
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateStatus(Guid id, UpdateEnrollmentStatusDto dto)
        {
            var enrollment = await _repository.GetByIdAsync(id);
            enrollment.UpdatedAt = DateTime.Now;
            // the mapper will copy the values in it to the entity
            // ef core tracks the changes and update only only specific attributes
            EnrollmentMapper.ToUpdateStatus(dto, enrollment);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateGrades(Guid id, UpdateEnrollmentGradesDto dto)
        {
            var enrollment = await _repository.GetByIdAsync(id);
            enrollment.UpdatedAt = DateTime.Now;
            // the mapper will copy the values in it to the entity
            // ef core tracks the changes and update only only specific attributes
            EnrollmentMapper.ToUpdateGrades(dto, enrollment);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<SemesterTranscriptDto>> GetStudentGradesAsync(Guid studentId)
        {
            var groupedModels = await _repository.GetEnrollmentCourseGradesGroupedBySemester(studentId);

            var transcript = groupedModels.Select(group => new SemesterTranscriptDto
            {
                SemesterName = group.Key,
                Courses = group.Select(model => new SemesterGradesDto
                {
                    SemesterId = model.SemesterId,
                    SemesterName = model.SemesterName,
                    CourseCode = model.CourseCode,
                    CourseName = model.CourseName,
                    TotalGrade = model.TotalGrade
                }).ToList()
            }).ToList();

            return transcript;
        }
    }
}
