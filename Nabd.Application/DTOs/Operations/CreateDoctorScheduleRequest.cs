using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    public class CreateDoctorScheduleRequest
    {
        // ==========================================
        // 1. Context (المكان)
        // ==========================================

        [Required(ErrorMessage = "يجب تحديد الفرع.")]
        public Guid ClinicBranchId { get; set; }

        // ==========================================
        // 2. Timing (الزمان)
        // ==========================================

        [Required(ErrorMessage = "اليوم مطلوب.")]
        [Range(0, 6, ErrorMessage = "اليوم غير صالح (0 = الأحد، 6 = السبت).")]
        public DayOfWeek DayOfWeek { get; set; }

        [Required(ErrorMessage = "وقت البدء مطلوب.")]
        public TimeSpan StartTime { get; set; } // صيغة JSON: "09:00:00"

        [Required(ErrorMessage = "وقت الانتهاء مطلوب.")]
        public TimeSpan EndTime { get; set; }

        // ==========================================
        // 3. Operational Rules (قواعد الحجز)
        // ==========================================

        [Required]
        [Range(5, 120, ErrorMessage = "مدة الكشف يجب أن تكون بين 5 و 120 دقيقة.")]
        public int SlotDurationMinutes { get; set; } = 30;

        // ==========================================
        // 4. Break Time (فترات الراحة - Enterprise Feature)
        // ==========================================

        public bool HasBreak { get; set; } = false;

        public TimeSpan? BreakStartTime { get; set; }

        public TimeSpan? BreakEndTime { get; set; }
    }
}