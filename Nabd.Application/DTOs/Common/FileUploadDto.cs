using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Common
{
    public class FileUploadDto
    {
        [Required(ErrorMessage = "الملف مطلوب.")]
        public required IFormFile File { get; set; }

        public string? FileName { get; set; } // اسم اختياري لو عايز تغير اسم الملف
        public string? Description { get; set; } // وصف للملف (مثلاً: "شهادة تخرج")
    }
}