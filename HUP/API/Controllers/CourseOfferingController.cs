using Microsoft.AspNetCore.Mvc;
using HUP.Application.Mappers;
using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Entities.Academics;
using HUP.Application.Services.Interfaces;

namespace HUP.API
{
   [ApiController]
   [Route("api/[controller]")]
   public class CourseOfferingController : ControllerBase
   {
       private readonly ICourseOfferingService _service;

       public CourseOfferingController(ICourseOfferingService service)
       {
           _service = service;
       }

       // GET: api/CourseOffering
       [HttpGet]
       public async Task<ActionResult<IEnumerable<CourseOfferingDto>>> GetAll()
       {
           var courseOfferings = await _service.GetAllAsync();
           return Ok(courseOfferings);
       }

       // GET: api/CourseOffering/{id}
       [HttpGet("{id}")]
       public async Task<ActionResult<CourseOfferingDto>> GetById(Guid id)
       {
           var courseOffering = await _service.GetByIdAsync(id);
           if (courseOffering == null)
           {
               return NotFound();
           }
           return Ok(courseOffering);
       }

       // GET: api/CourseOffering/active/{departmentId}/{semesterId}
       [HttpGet("active/{departmentId}/{semesterId}")]
       public async Task<ActionResult<IEnumerable<CourseOfferingDto>>> GetActiveCourseOfferings(Guid departmentId, Guid semesterId)
       {
           var courseOfferings = await _service.GetActiveCourseOfferingAsync(departmentId, semesterId);
           return Ok(courseOfferings);
       }

       // GET: api/CourseOffering/available/{studentId}
       [HttpGet("available/{studentId}")]
       public async Task<ActionResult<IEnumerable<CourseOfferingDto>>> GetAvailableToRegister(Guid studentId)
       {
           var courseOfferings = await _service.GetAvailableToRegisterAsync(studentId);
           return Ok(courseOfferings);
       }

       // POST: api/CourseOffering
       [HttpPost]
       public async Task<ActionResult<CourseOfferingDto>> Create([FromBody] CreateCourseOfferingDto createDto)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }
           var createdCourseOffering = _service.AddAsync(createDto);
           return CreatedAtAction(nameof(GetById), new { id = createdCourseOffering.Id }, createdCourseOffering);
       }

       // PUT: api/CourseOffering/{id}
       [HttpPut("{id}")]
       public async Task<IActionResult> Update(Guid id, [FromBody] CreateCourseOfferingDto updateDto)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }

           var existingCourseOffering = await _service.GetByIdAsync(id);
           if (existingCourseOffering == null)
           {
               return NotFound();
           }

           await _service.Update(updateDto);
           return NoContent();
       }

       // DELETE: api/CourseOffering/{id}
       [HttpDelete("{id}")]
       public async Task<IActionResult> Delete(Guid id)
       {
           var courseOffering = await _service.GetByIdAsync(id);
           if (courseOffering == null)
           {
               return NotFound();
           }
           _service.SoftDelete(id);
           return NoContent();
       }

       // DELETE: api/CourseOffering/{id}/hard
       [HttpDelete("{id}/hard")]
       public async Task<IActionResult> HardDelete(Guid id)
       {
           var courseOffering = await _service.GetByIdAsync(id);
           if (courseOffering == null)
           {
               return NotFound();
           }
           _service.Remove(id);
           return NoContent();
       }
   }
}

