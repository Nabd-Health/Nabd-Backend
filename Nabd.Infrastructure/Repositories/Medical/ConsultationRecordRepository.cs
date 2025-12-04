using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Interfaces.Repositories.Medical;
using Nabd.Infrastructure.Data;
using System;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Medical
{
    public class ConsultationRecordRepository : GenericRepository<ConsultationRecord>, IConsultationRecordRepository
    {
        public ConsultationRecordRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<ConsultationRecord?> GetByAppointmentIdAsync(Guid appointmentId)
        {
            // الهدف: عرض تفاصيل الكشف كاملة في واجهة الطبيب
            return await _dbSet
                .Include(c => c.Prescriptions)
                    .ThenInclude(p => p.PrescriptionItems) // عشان نعرض الأدوية اللي اتكتبت
                .Include(c => c.Attachments) // الأشعة والتحاليل المرفقة
                .Include(c => c.AIDiagnosisLogs) // اقتراحات الـ AI السابقة لهذا الكشف
                .FirstOrDefaultAsync(c => c.AppointmentId == appointmentId);
        }

        public async Task<ConsultationRecord?> GetForDiagnosisAnalysisAsync(Guid consultationId)
        {
            // الهدف: تجميع البيانات اللازمة لموديل الـ AI (Feedback Loop)
            // الـ AI محتاج يعرف: أعراض المريض + تشخيص الدكتور النهائي + الأدوية اللي اتكتبت + سن وجنس المريض

            return await _dbSet
                .Include(c => c.Appointment)
                    .ThenInclude(a => a.Patient) // ضروري عشان نعرف السن والجنس (Demographics)
                .Include(c => c.Prescriptions)
                    .ThenInclude(p => p.PrescriptionItems) // عشان الـ AI يعرف "إيه الدواء المناسب لهذا التشخيص"
                .Include(c => c.AIDiagnosisLogs) // عشان نقارن اقتراح الـ AI بقرار الدكتور النهائي
                .FirstOrDefaultAsync(c => c.Id == consultationId);
        }
    }
}