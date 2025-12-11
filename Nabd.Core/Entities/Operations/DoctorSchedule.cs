using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; 
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Operations
{
    public class DoctorSchedule : BaseEntity
    {
        // ==========================================
        // 1. Context
        // ==========================================

        public Guid DoctorId { get; set; }
        public required Doctor Doctor { get; set; }

        public Guid ClinicBranchId { get; set; } 
        public required ClinicBranch ClinicBranch { get; set; }

        public DayOfWeek DayOfWeek { get; set; } 

        // ==========================================
        // 2. Time Slots
        // ==========================================
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        // ==========================================
        // 3. Breaks  - Enterprise Feature
        // ==========================================
       

        public bool HasBreak { get; set; } = false;
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }

        // ==========================================
        // 4. Capacity & Rules
        // ==========================================

    
        public int MaxPatientsPerDay { get; set; } = 50;

       
        public int SlotDurationMinutes { get; set; } = 30;

       
        public bool IsDayOff { get; set; } = false;
    }
}