using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.Mappers;
using HUP.Application.Services.Interfaces;
using HUP.Core.Entities.Academics;
using HUP.Core.Enums;
using HUP.Repositories.Interfaces;

namespace HUP.Application.Services.Implementations
{
    public class StudentRegistrationService : IStudentRegistrationService
    {
        private readonly IClassGroupRepository _classGroupRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IStudentRegistrationRepository _registrationRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ICourseOfferingRepository _courseOfferingRepo;
        private readonly ISemesterRepository _semesterRepo;
        private readonly ILogger<StudentRegistrationService> _logger;

        public StudentRegistrationService(
            IClassGroupRepository classGroupRepo,
            IEnrollmentRepository enrollmentRepo,
            IStudentRegistrationRepository registrationRepo,
            IStudentRepository studentRepo,
            ICourseRepository courseRepo,
            ICourseOfferingRepository courseOfferingRepo,
            ISemesterRepository semesterRepo,
            ILogger<StudentRegistrationService> logger)
        {
            _classGroupRepo = classGroupRepo;
            _enrollmentRepo = enrollmentRepo;
            _registrationRepo = registrationRepo;
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _courseOfferingRepo = courseOfferingRepo;
            _semesterRepo = semesterRepo;
            _logger = logger;
        }

        public async Task<List<AvailableCourseDto>> GetAvailableCoursesAsync(Guid studentId)
        {
            try
            {
                _logger.LogInformation($"Getting available courses for student: {studentId}");

                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    throw new KeyNotFoundException($"Student with ID {studentId} not found");

                var currentSemester = await _semesterRepo.GetActiveSemesterAsync();
                if (currentSemester == null)
                    throw new InvalidOperationException("No active semester found");

                var completedCourses = await _registrationRepo.GetCompletedCoursesAsync(studentId);
                var currentEnrollments = await _enrollmentRepo.GetRegisteredByStudentAsync(studentId);
                var currentCourseIds = currentEnrollments
                    .Select(e => e.ClassGroup.CourseOffering.CourseId)
                    .Distinct()
                    .ToList();

                var departmentCourses = await _courseRepo.GetCoursesByDepartmentAsync(student.DepartmentId);

                var levelCourses = departmentCourses
                    .Where(c => c.Level <= student.Level)
                    .OrderBy(c => c.Level)
                    .ThenBy(c => c.CourseCode)
                    .ToList();

                var result = new List<AvailableCourseDto>();

                foreach (var course in levelCourses)
                {
                    if (currentCourseIds.Contains(course.Id))
                        continue;

                    bool prerequisiteMet = true;
                    string prerequisiteCourseName = null;

                    if (course.PrerequisiteId.HasValue)
                    {
                        var prerequisiteCourse = await _courseRepo.GetByIdAsync(course.PrerequisiteId.Value);
                        prerequisiteCourseName = prerequisiteCourse?.CourseName ?? "Unknown";
                        prerequisiteMet = completedCourses.Contains(course.PrerequisiteId.Value);

                        if (!prerequisiteMet)
                            continue; 
                    }

                    var courseOfferings = await _courseOfferingRepo.GetActiveCourseOfferingAsync(
                        student.DepartmentId, currentSemester.Id);

                    var offerings = courseOfferings
                        .Where(co => co.CourseId == course.Id)
                        .ToList();

                    if (!offerings.Any())
                        continue; 

                    var availableGroups = new List<ClassGroupDto>();

                    foreach (var offering in offerings)
                    {
                        var groups = await _classGroupRepo.GetByCourseOfferingAsync(offering.Id);

                        var validGroups = groups
                            .Where(g => g.IsActive
                                     && !g.IsDeleted
                                     && g.CurrentEnrollment < g.Capacity)
                            .Select(ClassGroupMapper.ToDto)
                            .ToList();

                        availableGroups.AddRange(validGroups);
                    }

                    if (availableGroups.Any())
                    {
                        result.Add(new AvailableCourseDto
                        {
                            CourseId = course.Id,
                            CourseCode = course.CourseCode,
                            CourseName = course.CourseName,
                            Credits = course.Credits,
                            Level = course.Level,
                            HasPrerequisite = course.PrerequisiteId.HasValue,
                            PrerequisiteCourse = prerequisiteCourseName,
                            PrerequisiteMet = prerequisiteMet,
                            AvailableGroups = availableGroups,
                            MaxAllowedGroups = 1 
                        });
                    }
                }

                _logger.LogInformation($"Found {result.Count} available courses for student {studentId}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting available courses for student {studentId}");
                throw;
            }
        }

        public async Task<RegistrationResultDto> RegisterCoursesAsync(RegistrationRequestDto request)
        {
            var result = new RegistrationResultDto
            {
                Success = false,
                RegisteredGroupIds = new List<Guid>(),
                Errors = new List<string>(),
                Warnings = new List<string>()
            };

            try
            {
                _logger.LogInformation($"Starting registration for student: {request.StudentId}");

                var student = await _studentRepo.GetByIdAsync(request.StudentId);
                if (student == null)
                {
                    result.Errors.Add("الطالب غير موجود");
                    return result;
                }

                var allowedCredits = await CalculateAllowedCreditsAsync(request.StudentId);
                var currentCredits = await GetCurrentCreditsAsync(request.StudentId);
                var requestedCredits = await CalculateRequestedCreditsAsync(request.SelectedGroupIds);

                if (currentCredits + requestedCredits > allowedCredits)
                {
                    result.Errors.Add($"تجاوز الحد الأقصى للساعات المسموح بها. المسموح: {allowedCredits}، الحالي: {currentCredits}، المطلوب: {requestedCredits}");
                    return result;
                }

                var validationResult = await ValidateRegistrationAsync(request.StudentId, request.SelectedGroupIds);
                if (!validationResult)
                {
                    result.Errors.Add("فشل التحقق من صحة التسجيل. قد يكون هناك تعارض زمني أو تجاوز للطاقة الاستيعابية");
                    return result;
                }

                foreach (var groupId in request.SelectedGroupIds)
                {
                    var group = await _classGroupRepo.GetByIdAsync(groupId);
                    if (group == null)
                    {
                        result.Errors.Add($"المجموعة {groupId} غير موجودة");
                        continue;
                    }

                    if (await _registrationRepo.IsAlreadyEnrolledAsync(request.StudentId, group.CourseOffering.CourseId))
                    {
                        result.Errors.Add($"مسجل بالفعل في المادة {group.CourseOffering.Course.CourseCode}");
                        continue;
                    }

                    if (await _classGroupRepo.IsGroupFullAsync(groupId))
                    {
                        result.Errors.Add($"المجموعة {group.GroupCode} ممتلئة");
                        continue;
                    }

                    if (await _registrationRepo.CheckTimeConflictAsync(request.StudentId, groupId))
                    {
                        result.Errors.Add($"تعارض زمني مع المجموعة {group.GroupCode}");
                        continue;
                    }

                    var enrollment = new Enrollment
                    {
                        Id = Guid.NewGuid(),
                        StudentId = request.StudentId,
                        ClassGroupId = groupId,
                        EnrollmentDate = DateTime.Now,
                        Status = EnrollmentStatus.Registered,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false
                    };

                    try
                    {
                        await _enrollmentRepo.AddAsync(enrollment);
                        await _classGroupRepo.IncrementEnrollmentAsync(groupId);

                        result.RegisteredGroupIds.Add(groupId);
                        _logger.LogInformation($"Successfully registered student {request.StudentId} in group {groupId}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error registering student {request.StudentId} in group {groupId}");
                        result.Errors.Add($"خطأ في تسجيل المجموعة {group.GroupCode}: {ex.Message}");
                    }
                }

                if (result.RegisteredGroupIds.Any())
                {
                    await _enrollmentRepo.SaveChangesAsync();
                    result.Success = true;
                    result.Message = $"تم التسجيل في {result.RegisteredGroupIds.Count} مادة بنجاح";

                    await UpdateStudentCGPAAsync(request.StudentId);
                }
                else
                {
                    result.Message = "لم يتم التسجيل في أي مادة";
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in registration process for student {request.StudentId}");
                result.Errors.Add($"خطأ في عملية التسجيل: {ex.Message}");
                return result;
            }
        }

        public async Task<DropCourseResultDto> DropCourseAsync(Guid studentId, Guid enrollmentId)
        {
            var result = new DropCourseResultDto
            {
                Success = false,
                Message = string.Empty
            };

            try
            {
                var enrollment = await _enrollmentRepo.GetByIdAsync(enrollmentId);
                if (enrollment == null || enrollment.StudentId != studentId)
                {
                    result.Message = "التسجيل غير موجود أو لا ينتمي للطالب";
                    return result;
                }

                var group = await _classGroupRepo.GetByIdAsync(enrollment.ClassGroupId);
                var semester = group?.CourseOffering?.Semester;

                if (semester != null && DateTime.Now > semester.StartDate.AddDays(14))
                {
                    result.Message = "لقد انتهت فترة الإسقاط (أسبوعين من بداية الفصل)";
                    return result;
                }

                enrollment.Status = EnrollmentStatus.Dropped;
                enrollment.UpdatedAt = DateTime.Now;

                await _classGroupRepo.DecrementEnrollmentAsync(enrollment.ClassGroupId);

                await _enrollmentRepo.SaveChangesAsync();

                result.Success = true;
                result.Message = "تم إسقاط المادة بنجاح";
                result.DroppedEnrollmentId = enrollmentId;

                await UpdateStudentCGPAAsync(studentId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error dropping course for student {studentId}, enrollment {enrollmentId}");
                result.Message = $"خطأ في عملية الإسقاط: {ex.Message}";
                return result;
            }
        }

        public async Task<StudentRegistrationSummaryDto> GetRegistrationSummaryAsync(Guid studentId)
        {
            var summary = new StudentRegistrationSummaryDto
            {
                StudentId = studentId,
                CurrentSemester = string.Empty,
                RegistrationDeadline = DateTime.MinValue,
                RegisteredCourses = new List<RegisteredCourseDto>(),
                AvailableCredits = 0,
                UsedCredits = 0,
                RemainingCredits = 0,
                CanRegister = false
            };

            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return summary;

                var currentSemester = await _semesterRepo.GetActiveSemesterAsync();
                if (currentSemester == null)
                    return summary;

                summary.CurrentSemester = currentSemester.SemesterName;
                summary.RegistrationDeadline = currentSemester.RegistrationDeadline;
                summary.CanRegister = DateTime.Now <= currentSemester.RegistrationDeadline;

                summary.AvailableCredits = await CalculateAllowedCreditsAsync(studentId);
                summary.UsedCredits = await GetCurrentCreditsAsync(studentId);
                summary.RemainingCredits = summary.AvailableCredits - summary.UsedCredits;

                var enrollments = await _enrollmentRepo.GetRegisteredByStudentAsync(studentId);

                foreach (var enrollment in enrollments)
                {
                    var group = enrollment.ClassGroup;
                    var course = group.CourseOffering.Course;

                    summary.RegisteredCourses.Add(new RegisteredCourseDto
                    {
                        EnrollmentId = enrollment.Id,
                        CourseCode = course.CourseCode,
                        CourseName = course.CourseName,
                        Credits = course.Credits,
                        GroupCode = group.GroupCode,
                        DayOfWeek = group.DayOfWeek.ToString(),
                        StartTime = group.StartTime,
                        EndTime = group.EndTime,
                        Location = group.Location,
                        Room = group.Room,
                        InstructorName = group.Instructor?.User?.FullName ?? "غير محدد",
                        EnrollmentStatus = enrollment.Status.ToString(),
                        CanDrop = DateTime.Now <= currentSemester.StartDate.AddDays(14) 
                    });
                }

                return summary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting registration summary for student {studentId}");
                return summary;
            }
        }

        public async Task<List<ClassGroupDto>> GetStudentScheduleAsync(Guid studentId)
        {
            try
            {
                var groups = await _classGroupRepo.GetByStudentAsync(studentId);
                return ClassGroupMapper.ToDto(groups).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting schedule for student {studentId}");
                return new List<ClassGroupDto>();
            }
        }

        public async Task<bool> ValidateRegistrationAsync(Guid studentId, List<Guid> groupIds)
        {
            if (groupIds == null || !groupIds.Any())
                return false;

            try
            {
                var student = await _studentRepo.GetByIdAsync(studentId);
                if (student == null)
                    return false;

                var selectedGroups = new List<ClassGroup>();
                foreach (var groupId in groupIds)
                {
                    var group = await _classGroupRepo.GetByIdAsync(groupId);
                    if (group == null || group.IsDeleted || !group.IsActive)
                        return false;

                    selectedGroups.Add(group);
                }

                for (int i = 0; i < selectedGroups.Count; i++)
                {
                    for (int j = i + 1; j < selectedGroups.Count; j++)
                    {
                        var group1 = selectedGroups[i];
                        var group2 = selectedGroups[j];

                        if (group1.DayOfWeek == group2.DayOfWeek)
                        {
                            bool timeConflict = (group1.StartTime >= group2.StartTime && group1.StartTime < group2.EndTime) ||
                                                (group1.EndTime > group2.StartTime && group1.EndTime <= group2.EndTime) ||
                                                (group1.StartTime <= group2.StartTime && group1.EndTime >= group2.EndTime);

                            if (timeConflict)
                                return false;
                        }
                    }
                }

                foreach (var group in selectedGroups)
                {
                    if (group.CurrentEnrollment >= group.Capacity)
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error validating registration for student {studentId}");
                return false;
            }
        }

        public async Task<int> CalculateAllowedCreditsAsync(Guid studentId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            if (student == null)
                return 0;

            var baseCredits = student.Cgpa >= 3.0m ? 18 : 15;

            var levelBonus = student.Level switch
            {
                1 => 0,
                2 => 3,
                3 => 6,
                4 => 9,
                _ => 0
            };

            return baseCredits + levelBonus;
        }


        private async Task<int> GetCurrentCreditsAsync(Guid studentId)
        {
            var enrollments = await _enrollmentRepo.GetRegisteredByStudentAsync(studentId);
            var courseIds = enrollments
                .Select(e => e.ClassGroup.CourseOffering.CourseId)
                .Distinct()
                .ToList();

            if (!courseIds.Any())
                return 0;

            var courses = await _courseRepo.GetCoursesByIdsAsync(courseIds);
            return courses.Sum(c => c.Credits);
        }

        private async Task<int> CalculateRequestedCreditsAsync(List<Guid> groupIds)
        {
            if (groupIds == null || !groupIds.Any())
                return 0;

            var courseIds = new List<Guid>();
            foreach (var groupId in groupIds)
            {
                var group = await _classGroupRepo.GetByIdAsync(groupId);
                if (group != null)
                {
                    courseIds.Add(group.CourseOffering.CourseId);
                }
            }

            var distinctCourseIds = courseIds.Distinct().ToList();
            if (!distinctCourseIds.Any())
                return 0;

            var courses = await _courseRepo.GetCoursesByIdsAsync(distinctCourseIds);
            return courses.Sum(c => c.Credits);
        }

        private async Task UpdateStudentCGPAAsync(Guid studentId)
        {
            try
            {
                var enrollments = await _enrollmentRepo.GetByStudentId(studentId);
                var completedEnrollments = enrollments
                    .Where(e => e.Status == EnrollmentStatus.Completed)
                    .ToList();

                if (!completedEnrollments.Any())
                    return;

                decimal totalPoints = 0;
                decimal totalCredits = 0;

                foreach (var enrollment in completedEnrollments)
                {
                    var course = enrollment.ClassGroup.CourseOffering.Course;
                    var totalGrade = enrollment.ClassGrade + enrollment.MidtermGrade + enrollment.FinalGrade;

                    var gradePoints = CalculateGradePoints(totalGrade);

                    totalPoints += gradePoints * course.Credits;
                    totalCredits += course.Credits;
                }

                if (totalCredits > 0)
                {
                    var cgpa = totalPoints / totalCredits;

                    var student = await _studentRepo.GetByIdAsync(studentId);
                    if (student != null)
                    {
                        student.Cgpa = Math.Round(cgpa, 2);
                        await _studentRepo.UpdateAsync(student);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating CGPA for student {studentId}");
            }
        }

        private decimal CalculateGradePoints(decimal totalGrade)
        {
            return totalGrade switch
            {
                >= 90 => 4.0m,
                >= 85 => 3.75m,
                >= 80 => 3.4m,
                >= 75 => 3.1m,
                >= 70 => 2.8m,
                >= 65 => 2.5m,
                >= 60 => 2.2m,
                >= 50 => 2.0m,
                _ => 0.0m
            };
        }
    }
}
