using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Profiles
{
    public class UpdateDoctorProfileRequest
    {
        // ==========================================
        // 1. Marketing & Personal Info 
        // ==========================================

        [MaxLength(100, ErrorMessage = "الاسم لا يجب أن يتجاوز 100 حرف.")]
        public string? FullName { get; set; } 

        [MaxLength(1500, ErrorMessage = "النبذة التعريفية طويلة جداً.")]
        public string? Bio { get; set; } 

        [Phone(ErrorMessage = "رقم الهاتف غير صحيح.")]
        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; } 

        // ==========================================
        // 2. Location Info 
        // ==========================================

        [MaxLength(200)]
        public string? Address { get; set; } 

        [MaxLength(50)]
        public string? City { get; set; }

        // ==========================================
        // 3. Operational Settings
        // ==========================================

        [Range(0, 10000, ErrorMessage = "سعر الكشف يجب أن يكون منطقياً.")]
        public decimal? ConsultationFee { get; set; } 

        [Range(5, 180, ErrorMessage = "مدة الكشف يجب أن تكون بين 5 دقائق و 3 ساعات.")]
        public int? SessionDurationMinutes { get; set; } 

       
    }
}