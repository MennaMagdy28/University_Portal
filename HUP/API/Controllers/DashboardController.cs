using HUP.Application.DTOs.DashboardDTOs;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return Ok(stats);
        }

        [HttpGet("academic-reports")]
        public async Task<ActionResult<IEnumerable<AcademicReportDto>>> GetAcademicReports()
        {
            var reports = await _dashboardService.GetAcademicReportsAsync();
            return Ok(reports);
        }
    }
}
