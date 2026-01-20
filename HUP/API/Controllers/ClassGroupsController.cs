using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassGroupsController : ControllerBase
    {
        private readonly IClassGroupService _service;

        public ClassGroupsController(IClassGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassGroupDto>>> GetAll()
        {
            var groups = await _service.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassGroupDto>> GetById(Guid id)
        {
            var group = await _service.GetByIdAsync(id);
            if (group == null)
                return NotFound();

            return Ok(group);
        }

        [HttpGet("course-offering/{courseOfferingId}")]
        public async Task<ActionResult<IEnumerable<ClassGroupDto>>> GetByCourseOffering(Guid courseOfferingId)
        {
            var groups = await _service.GetByCourseOfferingAsync(courseOfferingId);
            return Ok(groups);
        }

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<ClassGroupDto>>> GetByInstructor(Guid instructorId)
        {
            var groups = await _service.GetByInstructorAsync(instructorId);
            return Ok(groups);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,DepartmentHead")]
        public async Task<ActionResult<ClassGroupDto>> Create([FromBody] CreateClassGroupDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var group = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,DepartmentHead")]
        public async Task<ActionResult<ClassGroupDto>> Update(Guid id, [FromBody] UpdateClassGroupDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var group = await _service.UpdateAsync(id, dto);
                return Ok(group);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,DepartmentHead")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var success = await _service.DeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}/deactivate")]
        [Authorize(Roles = "Admin,DepartmentHead")]
        public async Task<IActionResult> Deactivate(Guid id)
        {
            try
            {
                var success = await _service.SoftDeleteAsync(id);
                if (!success)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}/availability")]
        public async Task<ActionResult> CheckAvailability(Guid id)
        {
            var isAvailable = await _service.IsGroupAvailableAsync(id);
            var availableSeats = await _service.GetAvailableSeatsAsync(id);

            return Ok(new
            {
                IsAvailable = isAvailable,
                AvailableSeats = availableSeats
            });
        }
    }
}
