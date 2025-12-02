using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; // عشان يشوف Doctor
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Operations
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

        public DayOfWeek DayOfWeek { get; set; } // (System.DayOfWeek: Sunday=0, Monday=1...)

        // ==========================================
        // 2. Time Slots (ساعات العمل)
        // ==========================================

        // بنستخدم TimeSpan عشان نعبر عن "وقت في اليوم" (من 9 ص لـ 5 م) بغض النظر عن التاريخ
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

        // الحد الأقصى للكشوفات في اليوم ده
        public int MaxPatientsPerDay { get; set; } = 50;

        // مدة الكشف في الفرع ده (مهم لتقسيم المواعيد Slots)
        public int SlotDurationMinutes { get; set; } = 30;

        // هل اليوم ده إجازة؟ (بدل ما نمسح السطر، بنعلمه إجازة مؤقتة)
        public bool IsDayOff { get; set; } = false;
    }
}