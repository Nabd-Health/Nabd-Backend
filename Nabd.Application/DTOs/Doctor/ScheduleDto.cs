using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic; // لـ DayOfWeek Enum

namespace Nabd.Application.DTOs.Doctor
{
    // هذا DTO يمثل شيفت عمل أسبوعي متكرر للطبيب (يوم/ساعة)
    public class ScheduleDto
    {
        // ==================================================
        // 1. Identification & Location
        // ==================================================

        public Guid? Id { get; set; } // Id للشيفت (لو بنعدل عليه)

        [Required(ErrorMessage = "Clinic Branch ID is required.")]
        public required Guid ClinicBranchId { get; set; } // يربط هذا الشيفت بالفرع المحدد

        // ==================================================
        // 2. Core Timing & Day
        // ==================================================

        // Day of the week (System.DayOfWeek: 0 = Sunday, 6 = Saturday)
        [Required(ErrorMessage = "Day of week is required.")]
        [Range(0, 6, ErrorMessage = "Day must be between 0 (Sunday) and 6 (Saturday).")]
        public required DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "Shift start time is required.")]
        [DataType(DataType.Time)]
        public required TimeSpan StartTime { get; set; } // مثال: 09:00:00

        [Required(ErrorMessage = "Shift end time is required.")]
        [DataType(DataType.Time)]
        public required TimeSpan EndTime { get; set; } // مثال: 17:00:00

        // ==================================================
        // 3. Operational Rules
        // ==================================================

        [Required(ErrorMessage = "Slot duration is required.")]
        // مدة الكشف في هذا الشيفت (مهم جداً لحساب الأماكن الفارغة)
        [Range(10, 120, ErrorMessage = "Duration must be between 10 and 120 minutes.")]
        public int SlotDurationMinutes { get; set; }

        // الحد الأقصى للمرضى (لضمان أن الدكتور لا يرهق)
        [Range(1, 200, ErrorMessage = "Maximum patients must be realistic.")]
        public int MaxPatientsPerDay { get; set; } = 50;

        public bool IsDayOff { get; set; } = false; // لو الدكتور عايز يعطل الشيفت ده بالكامل

        // ==================================================
        // 4. Breaks (Optional Split Shift)
        // ==================================================

        public bool HasBreak { get; set; } = false;

        // أوقات الراحة (يجب أن تكون Nullable)
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }

        // ==================================================
        // 5. Future Proofing (Validity Period)
        // ==================================================

        // صلاحية هذا الجدول (لو هيعمل جدول مؤقت مثلاً لرمضان)
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}