using System;

namespace Nabd.Application.DTOs.Operations
{
    public class DoctorScheduleResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Context (المكان)
        // ==========================================
        public Guid ClinicBranchId { get; set; }
        public string ClinicBranchName { get; set; } = string.Empty;

        // ==========================================
        // 2. Timing (الزمان)
        // ==========================================

        // اليوم (0 = Sunday, etc.)
        public DayOfWeek DayOfWeek { get; set; }

        // للعرض في الـ UI (مثال: "Sunday" أو "الأحد")
        public string DayName { get; set; } = string.Empty;

        // الأوقات بصيغة HH:mm للعرض (مثال: "09:00", "17:00")
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;

        // ==========================================
        // 3. Operational Rules
        // ==========================================
        public int SlotDurationMinutes { get; set; }
        public int MaxPatientsPerDay { get; set; }

        // ==========================================
        // 4. Break Time (فترات الراحة)
        // ==========================================
        public bool HasBreak { get; set; }
        public string? BreakStartTime { get; set; }
        public string? BreakEndTime { get; set; }

        // حالة الجدول (هل هو نشط أم إجازة؟)
        public bool IsDayOff { get; set; }
    }
}