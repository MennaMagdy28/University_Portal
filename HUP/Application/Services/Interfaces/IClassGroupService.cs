using HUP.Application.DTOs.AcademicDtos;

namespace HUP.Application.Services.Interfaces
{
    public interface IClassGroupService
    {
        Task<ClassGroupDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ClassGroupDto>> GetAllAsync();
        Task<IEnumerable<ClassGroupDto>> GetByCourseOfferingAsync(Guid courseOfferingId);
        Task<IEnumerable<ClassGroupDto>> GetByInstructorAsync(Guid instructorId);
        Task<ClassGroupDto> CreateAsync(CreateClassGroupDto dto);
        Task<ClassGroupDto> UpdateAsync(Guid id, UpdateClassGroupDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<bool> IsGroupAvailableAsync(Guid groupId);
        Task<int> GetAvailableSeatsAsync(Guid groupId);
    }
}
