using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSchedulesController : ControllerBase
    {
        private readonly ICourseScheduleService _courseScheduleService;

        public CourseSchedulesController(ICourseScheduleService courseScheduleService)
        {
            _courseScheduleService = courseScheduleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseScheduleDto>>> GetSchedules()
        {
            var schedules = await _courseScheduleService.GetAllAsync();
            return Ok(schedules);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseScheduleDto>> GetSchedule(Guid id)
        {
            var schedule = await _courseScheduleService.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();

            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult<CourseScheduleDto>> CreateSchedule(CourseScheduleCreateDto scheduleDto)
        {
            try
            {
                var schedule = await _courseScheduleService.CreateAsync(scheduleDto);
                return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, schedule);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseScheduleDto>> UpdateSchedule(Guid id, CourseScheduleUpdateDto scheduleDto)
        {
            try
            {
                var schedule = await _courseScheduleService.UpdateAsync(id, scheduleDto);
                return Ok(schedule);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(Guid id)
        {
            var result = await _courseScheduleService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
