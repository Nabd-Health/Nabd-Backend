using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; // Doctor
using Nabd.Core.Enums;            // Governorate
using Nabd.Core.Enums.Operations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Operations
{
    public class ClinicBranch : BaseEntity
    {
        // ==========================================
        // 1. Linkage (التبعية)
        // ==========================================

        public Guid DoctorId { get; set; }
        public virtual required Doctor Doctor { get; set; }

        // ==========================================
        // 2. Branch Identity (بيانات الفرع)
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } // مثال: "فرع الزقازيق - القومية"

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; } // الرقم الرئيسي للحجز

        public string? LandlineNumber { get; set; } // رقم أرضي (اختياري)

        // ==========================================
        // 3. Location Details (منقول ومطور من Address.cs)
        // ==========================================
        // دمجنا جدول العنوان هنا (Flattening) عشان الأداء يبقى طيارة

        public required Governorate Governorate { get; set; } // المحافظة (Enum) للفلترة

        [Required]
        [MaxLength(50)]
        public required string City { get; set; } // المدينة/الحي (مثال: "حي الزهور")

        [Required]
        [MaxLength(200)]
        public required string StreetAddress { get; set; } // الشارع ورقم العمارة

        // الخريطة (مهم جداً للـ Mobile App)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 4. Financials (تخصيص السعر)
        // ==========================================

        // لو الدكتور عايز يخلي الفرع ده أغلى من العادي
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CustomConsultationFee { get; set; }

        // ==========================================
        // 5. Status & Relations
        // ==========================================

        public bool IsActive { get; set; } = true;

        // مواعيد العمل في الفرع ده
        public virtual ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
    }
}