using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HUP.Repositories.Interfaces;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentRegistrationController : ControllerBase
    {
        private readonly IStudentRegistrationService _registrationService;
        private readonly IEnrollmentRepository _enrollmentRepo;

        public StudentRegistrationController(IStudentRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("available-courses")]
        public async Task<ActionResult<List<AvailableCourseDto>>> GetAvailableCourses()
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var courses = await _registrationService.GetAvailableCoursesAsync(studentId);
            return Ok(courses);
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResultDto>> RegisterCourses([FromBody] RegistrationRequestDto request)
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            request.StudentId = studentId;

            if (!ModelState.IsValid || request.SelectedGroupIds == null || !request.SelectedGroupIds.Any())
                return BadRequest("Please select at least one course");

            var result = await _registrationService.RegisterCoursesAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("registered-courses")]
        public async Task<ActionResult> GetRegisteredCourses()
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var enrollments = await _enrollmentRepo.GetRegisteredByStudentAsync(studentId);

            var result = enrollments.Select(e => new
            {
                CourseCode = e.ClassGroup.CourseOffering.Course.CourseCode,
                CourseName = e.ClassGroup.CourseOffering.Course.CourseName,
                GroupCode = e.ClassGroup.GroupCode,
                Day = e.ClassGroup.DayOfWeek.ToString(),
                Time = $"{e.ClassGroup.StartTime} - {e.ClassGroup.EndTime}",
                Location = e.ClassGroup.Location,
                Room = e.ClassGroup.Room,
                Instructor = e.ClassGroup.Instructor.User.FullName
            }).ToList();

            return Ok(result);
        }
    }
}
