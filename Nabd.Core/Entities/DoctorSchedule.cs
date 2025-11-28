using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities
{
    public class DoctorSchedule : BaseEntity
    {
        // ==========================================
        // 1. Context (المكان والزمان)
        // ==========================================

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public Guid ClinicBranchId { get; set; } // الجدول ده يخص أنهي فرع؟
        public required ClinicBranch ClinicBranch { get; set; }

        public DayOfWeek DayOfWeek { get; set; } // السبت، الأحد... (Built-in Enum)

        // ==========================================
        // 2. Time Slots (ساعات العمل)
        // ==========================================

        // TimeSpan أفضل من DateTime لأننا بنخزن "ساعة" فقط (من 9 ص لـ 5 م)
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // ==========================================
        // 3. Breaks (أوقات الراحة) - Enterprise Feature
        // ==========================================
        // عشان السيستم ميطلعش مواعيد وقت الصلاة أو الغداء

        public bool HasBreak { get; set; } = false;
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }

        // ==========================================
        // 4. Capacity & Rules (التحكم في الحجز)
        // ==========================================

        // الحد الأقصى للكشوفات في اليوم ده (مثال: 20 كشف بس)
        public int MaxPatientsPerDay { get; set; } = 50;

        // مدة الكشف في الفرع ده (ممكن تختلف عن العادي)
        public int SlotDurationMinutes { get; set; } = 30;

        // هل اليوم ده إجازة؟ (بدل ما نمسح السطر، بنعلمه إجازة)
        public bool IsDayOff { get; set; } = false;
    }
}