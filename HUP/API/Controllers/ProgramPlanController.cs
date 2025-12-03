using System.Security.Claims;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramPlanController : ControllerBase
    {
        private readonly IProgramPlanService _programPlanService;

        public ProgramPlanController(IProgramPlanService programPlanService)
        {
            _programPlanService = programPlanService;
        }

        [HttpGet("student/")]
        [Authorize]
        public async Task<IActionResult> GetProgramPlanByStudentId()
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _programPlanService.GetByDepartmentAsync(studentId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
