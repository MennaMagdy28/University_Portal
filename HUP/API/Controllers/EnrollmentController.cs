using System.Security.Claims;
using HUP.Application.DTOs.AcademicDtos;
using Microsoft.AspNetCore.Mvc;
using HUP.Application.Services.Interfaces;
using HUP.Application.DTOs.AcademicDtos.Enrollment;
using Microsoft.AspNetCore.Authorization;

namespace HUP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetAll()
        {
            var enrollments = await _service.GetAllAsync();
            return Ok(enrollments);
        }

        // GET: api/Enrollment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentResponseDto>> GetById(Guid id)
        {
            var enrollment = await _service.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }

        // POST: api/Enrollment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnrollmentDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            createDto.StudentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var exist = await _service.Exists(createDto);
            if (exist)
                return BadRequest("The Course is registered");
            await _service.AddAsync(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // Patch: api/Enrollment/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateEnrollmentStatusDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();
            await _service.UpdateStatus(id, updateDto);
            return NoContent();
        }

        // Patch: api/Enrollment/{id}/grades
        [HttpPatch("{id}/grades")]
        public async Task<IActionResult> UpdateGrades(Guid id, [FromBody] UpdateEnrollmentGradesDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();
            await _service.UpdateGrades(id, updateDto);
            return NoContent();
        }

        // DELETE: api/Enrollment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            var enrollment = await _service.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();
            await _service.SoftDelete(id);
            return NoContent();
        }

        // DELETE: api/Enrollment/{id}/hard
        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDelete(Guid id)
        {
            var enrollment = await _service.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();
            await _service.Remove(id);
            return NoContent();
        }

        // Get: api/Enrollment/AllGrades/{studentId}
        [Authorize]
        [HttpGet("AllGrades")]
        public async Task<IActionResult> GetStudentGrades()
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _service.GetStudentGradesAsync(studentId);
            return Ok(result);
        }

        // GET: api/Enrollment/Registered/{studentId}
        [HttpGet("Registered")]
        public async Task<ActionResult<IEnumerable<EnrollmentResponseDto>>> GetRegisteredByStudentAsync()
        {
            var studentId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _service.GetRegisteredByStudentAsync(studentId);
            return Ok(result);
        }
    }
}