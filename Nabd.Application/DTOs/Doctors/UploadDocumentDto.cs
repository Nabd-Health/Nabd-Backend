using Microsoft.AspNetCore.Http; 
using Nabd.Core.Enums;          
using Nabd.Core.Enums.Identity;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Doctors
{
    public class UploadDocumentDto
    {
        [Required(ErrorMessage = "يجب تحديد نوع الوثيقة (بطاقة، كارنيه، شهادة...).")]
        public DoctorDocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "يرجى اختيار الملف المراد رفعه.")]
        public IFormFile File { get; set; } = null!;
    }
}