using HUP.Application.DTOs.AcademicDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface IExamService
    {
        Task<ExamDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ExamDto>> GetAllAsync();
        Task<ExamDto> CreateAsync(ExamCreateDto examDto);
        Task<ExamDto> UpdateAsync(Guid id, ExamUpdateDto examDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
