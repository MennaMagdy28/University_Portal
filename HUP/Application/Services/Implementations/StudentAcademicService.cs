using AutoMapper;
using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class StudentAcademicService : IStudentAcademicService
    {

        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IExamRepository _examRepository;
        private readonly ICourseScheduleRepository _courseScheduleRepository;
        private readonly IMapper _mapper;

        public StudentAcademicService(
            IStudentRepository studentRepository,
            IUserRepository userRepository,
            IEnrollmentRepository enrollmentRepository,
            IExamRepository examRepository,
            ICourseScheduleRepository courseScheduleRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _examRepository = examRepository;
            _courseScheduleRepository = courseScheduleRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<StudentTimetableDto>> GetStudentTimetableAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            var schedules = await _courseScheduleRepository.GetByStudentAsync(studentId);


            if (!schedules.Any())
            {
                Console.WriteLine($"No schedules found for student {studentId}");

                var currentSemester = GetCurrentSemester();
                var enrollments = await _enrollmentRepository.GetByStudentAndSemesterAsync(studentId, currentSemester);
                Console.WriteLine($"Student enrollments: {enrollments.Count()}");

                foreach (var enrollment in enrollments)
                {
                    Console.WriteLine($"Enrolled in: {enrollment.CourseOffering.Course?.CourseCode} - {enrollment.CourseOffering.Course?.CourseName}");
                }
            }

            return schedules.Select(s => new StudentTimetableDto
            {
                DayOfWeek = GetArabicDay(s.DayOfWeek),
                StartTime = s.StartTime.ToTimeSpan(),
                EndTime = s.EndTime.ToTimeSpan(),
                CourseCode = s.CourseOffering.Course.CourseCode,
                CourseName = s.CourseOffering.Course.CourseName,
                InstructorName = s.Instructor.User.FullName,
                Location = s.Location,
                Room = s.Room
            }).ToList();
        }

        public async Task<IEnumerable<StudentExamScheduleDto>> GetStudentExamScheduleAsync(Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
                throw new KeyNotFoundException("Student not found");

            var currentSemester = GetCurrentSemester();
            var enrollments = await _enrollmentRepository.GetByStudentAndSemesterAsync(studentId, currentSemester);
            var courseIds = enrollments.Select(e => e.CourseOffering.CourseId).ToList();

            var exams = await _examRepository.GetByCoursesAsync(courseIds);

            return exams.Select(e => new StudentExamScheduleDto
            {
                ExamId = e.Id,
                CourseCode = e.CourseOffering.Course.CourseCode,
                CourseName = e.CourseOffering.Course.CourseName,
                ExamType = GetArabicExamType(e.ExamType),
                ExamDate = e.ExamDate.ToDateTime(TimeOnly.MinValue),
                ExamTime = e.ExamTime.ToTimeSpan(),
                Location = e.Location,
                Room = ExtractRoomNumber(e.Location),
                // InstructorName = GetCourseInstructor(e.CourseOffering),
                DayOfWeek = GetArabicDay(e.ExamDate.DayOfWeek)
            }).OrderBy(e => e.ExamDate).ThenBy(e => e.ExamTime).ToList();
        }

        private string GetArabicExamType(ExamType examType)
        {
            return examType switch
            {
                ExamType.Midterm => "امتحان منتصف الفصل",
                ExamType.Final => "امتحان نهاية الفصل",
                _ => examType.ToString()
            };
        }

        private string ExtractRoomNumber(string location)
        {
            if (string.IsNullOrEmpty(location))
                return "غير محدد";

            var roomMatch = System.Text.RegularExpressions.Regex.Match(location, @"قاعة\s*(\d+)");
            if (roomMatch.Success)
                return $"قاعة {roomMatch.Groups[1].Value}";

            return location;
        }

        // private string GetCourseInstructor(CourseOffering course)
        // {
        //     var instructorId = course?.Instructors?.FirstOrDefault();
        //     
        //     return instructor != null ? $"د. {instructor.?.FullName ?? "غير محدد"}" : "غير محدد";
        // }

        private string GetCurrentSemester()
        {
            var now = DateTime.Now;
            var year = now.Year;
            var semester = now.Month switch
            {
                >= 1 and <= 5 => "Spring",
                >= 6 and <= 8 => "Summer",
                _ => "Fall"
            };
            return $"{semester} {year}";
        }

        private string GetArabicDay(DayOfWeek day)
        {
            return day switch
            {
                DayOfWeek.Sunday => "الأحد",
                DayOfWeek.Monday => "الاثنين",
                DayOfWeek.Tuesday => "الثلاثاء",
                DayOfWeek.Wednesday => "الأربعاء",
                DayOfWeek.Thursday => "الخميس",
                DayOfWeek.Friday => "الجمعة",
                DayOfWeek.Saturday => "السبت",
                _ => day.ToString()
            };
        }
    }
}
