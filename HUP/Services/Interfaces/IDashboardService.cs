using HUP.Core.DTOs.DashboardDTOs;

namespace HUP.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<IEnumerable<AcademicReportDto>> GetAcademicReportsAsync();
    }
}
