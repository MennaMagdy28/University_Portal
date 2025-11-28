using HUP.Application.DTOs.AcademicDtos.ProgramPlan;
using HUP.Application.Services.Interfaces;
using HUP.Core.Enums;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class ProgramPlanService : IProgramPlanService
    {
        private readonly IProgramPlanRepository _repository;
        private readonly IStudentRepository _studentRepository;

        public ProgramPlanService(IProgramPlanRepository repository, IStudentRepository studentRepository)
        {
            _repository = repository;
            _studentRepository = studentRepository;
        }

        public async Task<TranscriptDto> GetByDepartmentAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new Exception("Student not found");

            var plans = await _repository.GetByDepartmentAsync(student.DepartmentId);

           var items = plans.Select(p => new ProgramPlanItemDto
            {
                Code = p.Course.CourseCode,
                CourseName = p.Course.CourseName,
                Hours = p.Course.Credits,
                Total = 0,
                Grade = "",
                IsCompulsory = p.IsCompulsory,
                RequirementType = p.RequirementType
            }).ToList();


            var result = new TranscriptDto
            {
                UniversityCompulsory   = items.Where(x => x.RequirementType == RequirementType.University && x.IsCompulsory).ToList(),
                UniversityElective     = items.Where(x => x.RequirementType == RequirementType.University && !x.IsCompulsory).ToList(),

                FacultyCompulsory      = items.Where(x => x.RequirementType == RequirementType.Faculty && x.IsCompulsory).ToList(),
                FacultyElective        = items.Where(x => x.RequirementType == RequirementType.Faculty && !x.IsCompulsory).ToList(),

                DepartmentCompulsory   = items.Where(x => x.RequirementType == RequirementType.Department && x.IsCompulsory).ToList(),
                DepartmentElective     = items.Where(x => x.RequirementType == RequirementType.Department && !x.IsCompulsory).ToList(),
            };

            return result;
        }
    }    
}