
using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace Nabd.Core.Entities
{
    public class Doctor : BaseEntity
    {
        // ==========================================
        // 1. Identity Link (الربط بالحساب)
        // ==========================================
        // ده المفتاح اللي بيربط بيانات الدكتور بجدول المستخدمين (Email, Password)
        public required string ApplicationUserId { get; set; }

        // ==========================================
        // 2. Personal Info (البيانات الشخصية)
        // ==========================================
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; } // Enum (Male, Female)

        [Phone]
        public required string PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; } // مهم للفلترة (Find Doctor by City)

        public string? ProfilePictureUrl { get; set; } // صورة البروفايل

        // ==========================================
        // 3. Professional Info (البيانات المهنية)
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Specialization { get; set; } // مثال: Cardiology, Internal Medicine

        [MaxLength(500)]
        public string? Bio { get; set; } // نبذة تسويقية تظهر للمريض

        // بيانات التوثيق (عشان الأدمن يوافق عليه)
        public string? MedicalLicenseNumber { get; set; } // رقم ترخيص مزاولة المهنة
        public string? GraduationUniversity { get; set; } // خريج جامعة إيه؟
        public int YearsOfExperience { get; set; } // عدد سنوات الخبرة

        // ==========================================
        // 4. Clinic & Financials (العيادة والماليات)
        // ==========================================

        // سعر الكشف (Decimal عشان الفلوس لازم دقة)
        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }

        // مدة الكشف المتوقعة (عشان نحسب المواعيد)
        public int SessionDurationMinutes { get; set; } = 30; // افتراضي 30 دقيقة

        // ==========================================
        // 5. System Status (حالة الحساب)
        // ==========================================

        public DoctorStatus Status { get; set; } = DoctorStatus.Pending; // حالته: في الانتظار، مفعل، موقوف
        public bool IsAvailable { get; set; } = true; // زرار "أنا متاح/مش متاح" للطوارئ

        // ==========================================
        // 6. Statistics (إحصائيات للأداء)
        // ==========================================
        // بنحسبهم ونخزنهم هنا عشان مش كل مرة نعمل Query تقيلة
        public double AverageRating { get; set; } = 0.0;
        public int TotalReviews { get; set; } = 0;

        // ==========================================
        // 7. Relationships (العلاقات)
        // ==========================================

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        // جداول العمل (أيام وساعات العمل) - هنحتاجها قدام
        // public ICollection<DoctorSchedule> Schedules { get; set; } 
        public ICollection<ClinicBranch> ClinicBranches { get; set; } = new List<ClinicBranch>();
    }
}