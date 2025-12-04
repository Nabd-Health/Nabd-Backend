using System;

namespace Nabd.Core.DTOs
{
    public class DoctorPatientDto
    {
        public Guid Id { get; set; }

        // البيانات الشخصية
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}"; // خاصية مساعدة للعرض

        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? ProfileImageUrl { get; set; }

        // بيانات الموقع
        public string? City { get; set; }
        public string? Governorate { get; set; }

        // إحصائيات طبية (تخص هذا الطبيب)
        public DateTime? LastVisitDate { get; set; } // آخر زيارة للعيادة دي
        public int TotalSessions { get; set; }       // عدد مرات الكشف عند الطبيب ده
        public decimal? Rating { get; set; }         // تقييم المريض (لو متاح)
    }
}