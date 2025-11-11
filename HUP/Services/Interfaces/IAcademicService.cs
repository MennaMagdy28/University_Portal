using HUP.Core.DTOs.AcademicDtos;
using HUP.Core.Models.AcademicModels;

namespace HUP.Services.Interfaces
{
    public interface IAcademicService
    {
        Task<Faculty> CreateFacultyAsync(FacultyCreateDto facultyDto);
        Task<Department> CreateDepartmentAsync(DepartmentCreateDto departmentDto);
        Task<ProgramEntity> CreateProgramAsync(ProgramCreateDto programDto);
        Task<Course> CreateCourseAsync(CourseCreateDto courseDto);
        Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<IEnumerable<ProgramEntity>> GetAllProgramsAsync();
        Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
