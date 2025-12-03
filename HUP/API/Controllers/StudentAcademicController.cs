using System.Security.Claims;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAcademicController : ControllerBase
    {
        private readonly IStudentAcademicService _studentAcademicService;

        public StudentAcademicController(IStudentAcademicService studentAcademicService)
        {
            _studentAcademicService = studentAcademicService;
        }

        [HttpGet("timetable/")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StudentTimetableDto>>> GetStudentTimetable()
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var timetable = await _studentAcademicService.GetStudentTimetableAsync(studentId);
                return Ok(timetable);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("exam-schedule/")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StudentExamScheduleDto>>> GetStudentExamSchedule()
        {
            try
            {
                var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var examSchedule = await _studentAcademicService.GetStudentExamScheduleAsync(studentId);
                return Ok(examSchedule);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
