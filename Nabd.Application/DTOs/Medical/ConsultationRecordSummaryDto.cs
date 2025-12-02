using System;

namespace Nabd.Application.DTOs.Medical
{
    public class ConsultationRecordSummaryDto
    {
        public Guid Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;

        // التشخيص النهائي لهذه الزيارة
        public string Diagnosis { get; set; } = string.Empty;

        // هل تم استخدام AI في هذه الزيارة؟
        public bool WasAIAssisted { get; set; }
    }
}