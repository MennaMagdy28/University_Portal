using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Models.AcademicModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicController : ControllerBase
    {
        private readonly IAcademicService _academicService;

        public AcademicController(IAcademicService academicService)
        {
            _academicService = academicService;
        }

        [HttpPost("faculties")]
        public async Task<ActionResult<Faculty>> CreateFaculty(FacultyCreateDto facultyDto)
        {
            var faculty = await _academicService.CreateFacultyAsync(facultyDto);
            return CreatedAtAction(nameof(GetFaculties), new { id = faculty.Id }, faculty);
        }

        [HttpPost("departments")]
        public async Task<ActionResult<Department>> CreateDepartment(DepartmentCreateDto departmentDto)
        {
            var department = await _academicService.CreateDepartmentAsync(departmentDto);
            return Ok(department);
        }

        [HttpPost("programs")]
        public async Task<ActionResult<ProgramEntity>> CreateProgram(ProgramCreateDto programDto)
        {
            var program = await _academicService.CreateProgramAsync(programDto);
            return Ok(program);
        }

        [HttpPost("courses")]
        public async Task<ActionResult<Course>> CreateCourse(CourseCreateDto courseDto)
        {
            var course = await _academicService.CreateCourseAsync(courseDto);
            return Ok(course);
        }

        [HttpGet("faculties")]
        public async Task<ActionResult<IEnumerable<Faculty>>> GetFaculties()
        {
            var faculties = await _academicService.GetAllFacultiesAsync();
            return Ok(faculties);
        }

        [HttpGet("departments")]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _academicService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("programs")]
        public async Task<ActionResult<IEnumerable<ProgramEntity>>> GetPrograms()
        {
            var programs = await _academicService.GetAllProgramsAsync();
            return Ok(programs);
        }

        [HttpGet("courses")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _academicService.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
