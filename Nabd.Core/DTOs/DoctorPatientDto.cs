using System;

namespace Nabd.Core.DTOs
{
    /// <summary>
    /// DTO خفيف وسريع لعرض قائمة المرضى في Dashboard الطبيب.
    /// (تم وضعه في Core لأنه يُستخدم كـ Return Type للـ Repository Interface)
    /// </summary>
    public class DoctorPatientDto
    {
        public Guid PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Governorate { get; set; }

        // Metrics (خصائص محسوبة داخل الاستعلام)
        public int TotalSessions { get; set; }
        public DateTime? LastVisitDate { get; set; }
        public decimal? Rating { get; set; }
    }
}