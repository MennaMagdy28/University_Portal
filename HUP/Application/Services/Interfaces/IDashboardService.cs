using HUP.Application.DTOs.DashboardDTOs;

namespace HUP.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<IEnumerable<AcademicReportDto>> GetAcademicReportsAsync();
    }
}
