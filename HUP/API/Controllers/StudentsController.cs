using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.DTOs.AcademicDtos.Enrollment;
using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.Services.Interfaces;
using HUP.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _studentService.AddStudent(createDto);
            return Ok("Added");
        }

        [HttpGet("Profile")]
        [Authorize]
        public async Task<ActionResult<StudentProfileDto>> GetProfile()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _studentService.GetStudentProfile(id);
            if (profile == null)
                return BadRequest("User not found.");
            return Ok(profile);
        }
    }
}
