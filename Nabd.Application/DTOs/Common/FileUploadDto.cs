using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Common
{
    public class FileUploadDto
    {
        [Required(ErrorMessage = "الملف مطلوب.")]
        public required IFormFile File { get; set; }

        public string? FileName { get; set; } 
        public string? Description { get; set; } 
    }
}