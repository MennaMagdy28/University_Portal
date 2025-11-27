using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;

        public ExamsController(IExamService examService)
        {
            _examService = examService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExams()
        {
            var exams = await _examService.GetAllAsync();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamDto>> GetExam(Guid id)
        {
            var exam = await _examService.GetByIdAsync(id);
            if (exam == null)
                return NotFound();

            return Ok(exam);
        }

        [HttpPost]
        public async Task<ActionResult<ExamDto>> CreateExam(ExamCreateDto examDto)
        {
            try
            {
                var exam = await _examService.CreateAsync(examDto);
                return CreatedAtAction(nameof(GetExam), new { id = exam.Id }, exam);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExamDto>> UpdateExam(Guid id, ExamUpdateDto examDto)
        {
            try
            {
                var exam = await _examService.UpdateAsync(id, examDto);
                return Ok(exam);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExam(Guid id)
        {
            var result = await _examService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
