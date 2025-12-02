using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers;

public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService service)
    {
        _studentService = service;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentProfileDto>> GetById(Guid id)
    {
        var profile = await _studentService.GetStudentProfile(id);
        if (profile == null)
            return BadRequest("User not found.");
        return Ok(profile);
    }

}