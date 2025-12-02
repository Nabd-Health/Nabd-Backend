using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; // عشان يشوف Patient
using Nabd.Core.Enums.Medical;     // عشان يشوف HistoryEventType
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class MedicalHistoryItem : BaseEntity
    {
        // ==========================================
        // 1. Linkage (التبعية)
        // ==========================================

        public Guid PatientId { get; set; }
        public virtual Patient Patient { get; set; } = null!;

        // ==========================================
        // 2. Event Details (تفاصيل الحدث التاريخي)
        // ==========================================

        public HistoryEventType EventType { get; set; } // (عملية، حساسية، تطعيم...)

        [Required]
        [MaxLength(200)]
        public required string Title { get; set; } // العنوان (مثال: "عملية استئصال اللوزتين")

        [MaxLength(1000)]
        public string? Details { get; set; } // التفاصيل (مثال: "تمت في مستشفى القاهرة عام 2015")

        // ==========================================
        // 3. Metadata (الزمن والأهمية)
        // ==========================================

        // متى حدث هذا الشيء؟ (مهم جداً للترتيب في الـ Timeline)
        public DateTime EventDate { get; set; }

        // هل هذا الحدث خطير/هام؟ (عشان يظهر بلون أحمر أو مميز للدكتور)
        public bool IsCritical { get; set; } = false;
    }
}