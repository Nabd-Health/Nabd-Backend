using System;
using System.Threading.Tasks;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Interfaces.Repositories.Base;

namespace Nabd.Core.Interfaces.Repositories.Medical
{
    // هذا الريبوزيتوري مسؤول عن إدارة السجلات الطبية (Consultation Records)
    public interface IConsultationRecordRepository : IGenericRepository<ConsultationRecord>
    {
        // 1. استرجاع سجل الكشف الطبي المرتبط بموعد محدد (One-to-One)
        Task<ConsultationRecord?> GetByAppointmentIdAsync(Guid appointmentId);

        // 2. [تعزيز لـ AI Core]: جلب سجل الكشف مع البيانات اللازمة لنموذج التشخيص
        Task<ConsultationRecord?> GetForDiagnosisAnalysisAsync(Guid consultationId);
    }
}