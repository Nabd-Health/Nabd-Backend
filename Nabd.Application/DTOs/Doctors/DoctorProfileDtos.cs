using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Doctors 
{
 
    public class UpdateDoctorProfileDto
    {
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string? PhoneNumber { get; set; }

        [MaxLength(1500, ErrorMessage = "النبذة التعريفية طويلة جداً")]
        public string? Bio { get; set; }

        [Range(0, 10000, ErrorMessage = "سعر الكشف غير منطقي")]
        public decimal? ConsultationFee { get; set; }

        public int? SessionDurationMinutes { get; set; }

        public string? ClinicAddress { get; set; }
        public string? City { get; set; }
    }


    public class UploadProfileImageDto
    {
        [Required(ErrorMessage = "يرجى اختيار صورة")]
        public IFormFile Image { get; set; } = null!;
    }

    
    public class UpdateAvailabilityDto
    {
        public bool IsAvailable { get; set; }
    }
}