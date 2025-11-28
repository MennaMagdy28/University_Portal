using HUP.Application.DTOs.AcademicDtos.ProgramPlan;
using HUP.Core.Entities.Academics;

namespace HUP.Application.Services.Interfaces
{
    public interface IProgramPlanService
    {
        Task<TranscriptDto> GetByDepartmentAsync(Guid StudentId);
    }
}
