using HUP.Application.DTOs.AcademicDtos;
using HUP.Application.DTOs.AcademicDtos.Enrollment;
using HUP.Application.DTOs.AcademicDtos.Student;
using HUP.Application.Services.Interfaces;
using HUP.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HUP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPut("{id}/academic-status")]
        public async Task<ActionResult<StudentResponseDto>> UpdateAcademicStatus(Guid id, [FromBody] AcademicStatus status)
        {
            try
            {
                var student = await _studentService.UpdateStudentAcademicStatusAsync(id, status);
                return Ok(student);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/upload-profile")]
        public async Task<ActionResult> UploadProfileImage(Guid id, IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("الرجاء اختيار صورة");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                    return BadRequest("نوع الملف غير مدعوم. الرجاء استخدام صورة (jpg, jpeg, png)");

                if (file.Length > 5 * 1024 * 1024)
                    return BadRequest("حجم الملف كبير جداً. الحد الأقصى 5MB");

                var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);

                var fileName = $"{id}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
                var filePath = Path.Combine(uploadsPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = $"/uploads/profiles/{fileName}";
                var result = await _studentService.UploadProfileImageAsync(id, relativePath);

                if (!result)
                    return NotFound("الطالب غير موجود");

                return Ok(relativePath);
            }
            catch (Exception ex)
            {
                return BadRequest($"خطأ في رفع الصورة: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _studentService.AddStudent(createDto);
            return Ok("Added");
        }

    }
}
