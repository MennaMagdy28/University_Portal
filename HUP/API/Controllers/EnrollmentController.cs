using HUP.Application.DTOs.AcademicDtos;
using Microsoft.AspNetCore.Mvc;
using HUP.Application.Services.Interfaces;
using HUP.Application.DTOs.AcademicDtos.Enrollment;

namespace HUP.API
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
            await _service.AddAsync(createDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Enrollment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEnrollmentDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();
            await _service.Update(id, updateDto);
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
    }
}