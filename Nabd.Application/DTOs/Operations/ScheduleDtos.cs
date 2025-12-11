using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Operations
{
    // 1. Request: إضافة أو تعديل جدول مواعيد
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
        public TimeSpan StartTime { get; set; } 

        [Required(ErrorMessage = "وقت الانتهاء مطلوب.")]
        public TimeSpan EndTime { get; set; }

        // ==========================================
        // 3. Operational Rules (قواعد الحجز)
        // ==========================================
        [Required]
        [Range(5, 120, ErrorMessage = "مدة الكشف يجب أن تكون بين 5 و 120 دقيقة.")]
        public int SlotDurationMinutes { get; set; } = 30;

    
        public bool IsDayOff { get; set; } = false;

        // ==========================================
        // 4. Break Time 
        // ==========================================
        public bool HasBreak { get; set; } = false;
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }
    }


    public class DoctorScheduleResponse
    {
        public Guid Id { get; set; }
        public string ClinicBranchName { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty; 
        public bool IsDayOff { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool HasBreak { get; set; }
        public string? BreakTime { get; set; } 
    }
}