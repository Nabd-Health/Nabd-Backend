using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities
{
    public class ClinicBranch : BaseEntity
    {
        // ==========================================
        // 1. Linkage (التبعية)
        // ==========================================

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        // ==========================================
        // 2. Branch Identity (بيانات الفرع)
        // ==========================================

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; } // مثال: "فرع القومية"، "فرع المهندسين"

        [Required]
        [MaxLength(250)]
        public required string Address { get; set; }

        [Required]
        [Phone]
        public required string PhoneNumber { get; set; } // رقم حجز الفرع ده تحديداً

        // ==========================================
        // 3. Geolocation (الخريطة - مهم للـ Find Doctor)
        // ==========================================

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; } // رابط مباشر

        // ==========================================
        // 4. Financials (تخصيص السعر)
        // ==========================================

        // هل سعر الكشف هنا مختلف عن السعر الأساسي للدكتور؟
        // لو null بناخد السعر من بروفايل الدكتور، لو فيه قيمة بناخدها
        [Column(TypeName = "decimal(18,2)")]
        public decimal? CustomConsultationFee { get; set; }

        // ==========================================
        // 5. Status
        // ==========================================

        public bool IsActive { get; set; } = true; // لو قفل الفرع ده مؤقتاً

        // ==========================================
        // 6. Relationships
        // ==========================================

        // مواعيد العمل الخاصة بالفرع ده
        public ICollection<DoctorSchedule> Schedules { get; set; } = new List<DoctorSchedule>();
    }
}