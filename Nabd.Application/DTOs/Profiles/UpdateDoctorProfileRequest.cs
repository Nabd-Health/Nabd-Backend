using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Profiles
{
    public class UpdateDoctorProfileRequest
    {
        // ==========================================
        // 1. Marketing & Personal Info (التسويق)
        // ==========================================

        [MaxLength(100, ErrorMessage = "الاسم لا يجب أن يتجاوز 100 حرف.")]
        public string? FullName { get; set; } // لتصحيح الاسم لو فيه خطأ

        [MaxLength(1500, ErrorMessage = "النبذة التعريفية طويلة جداً.")]
        public string? Bio { get; set; } // "أستاذ دكتور بجامعة القاهرة..."

        [Phone(ErrorMessage = "رقم الهاتف غير صحيح.")]
        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; } // رابط الصورة بعد رفعها

        // ==========================================
        // 2. Location Info (الموقع العام)
        // ==========================================

        [MaxLength(200)]
        public string? Address { get; set; } // العنوان الشخصي أو العام

        [MaxLength(50)]
        public string? City { get; set; }

        // ==========================================
        // 3. Operational Settings (إعدادات التشغيل)
        // ==========================================

        [Range(0, 10000, ErrorMessage = "سعر الكشف يجب أن يكون منطقياً.")]
        public decimal? ConsultationFee { get; set; } // تحديث السعر الأساسي

        [Range(5, 180, ErrorMessage = "مدة الكشف يجب أن تكون بين 5 دقائق و 3 ساعات.")]
        public int? SessionDurationMinutes { get; set; } // تحديث مدة الكشف الافتراضية

        // ⚠️ ملاحظة هامة:
        // تم استبعاد (Specialization, MedicalLicenseNumber)
        // لأن تغييرهم يتطلب إعادة تقديم وثائق ومراجعة من الإدارة (Verifier).
    }
}