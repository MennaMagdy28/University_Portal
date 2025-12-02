using HUP.Application.Services.Interfaces;
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

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetProgramPlanByStudentId(Guid studentId)
        {
            var result = await _programPlanService.GetByDepartmentAsync(studentId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
