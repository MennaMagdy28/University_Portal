using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.AcademicModels;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class AcademicService : IAcademicService
    {
        private readonly IFacultyRepository _facultyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public AcademicService(
            IFacultyRepository facultyRepository,
            IDepartmentRepository departmentRepository,
            IProgramRepository programRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _facultyRepository = facultyRepository;
            _departmentRepository = departmentRepository;
            _programRepository = programRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        public async Task<Faculty> CreateFacultyAsync(FacultyCreateDto facultyDto)
        {
            var faculty = _mapper.Map<Faculty>(facultyDto);
            await _facultyRepository.AddAsync(faculty);
            return faculty;
        }

        public async Task<Department> CreateDepartmentAsync(DepartmentCreateDto departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.AddAsync(department);
            return department;
        }

        public async Task<ProgramEntity> CreateProgramAsync(ProgramCreateDto programDto)
        {
            var program = _mapper.Map<ProgramEntity>(programDto);
            await _programRepository.AddAsync(program);
            return program;
        }

        public async Task<Course> CreateCourseAsync(CourseCreateDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            await _courseRepository.AddAsync(course);
            return course;
        }

        public async Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
        {
            return await _facultyRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ProgramEntity>> GetAllProgramsAsync()
        {
            return await _programRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepository.GetAllAsync();
        }
    }
}
