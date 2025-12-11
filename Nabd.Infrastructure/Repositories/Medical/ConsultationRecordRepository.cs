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

     
        public async Task<ConsultationRecord?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Prescriptions)
                    .ThenInclude(p => p.PrescriptionItems) 
                .Include(c => c.Attachments) 
                .Include(c => c.AIDiagnosisLogs) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

       
        public async Task<ConsultationRecord?> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _dbSet
                .Include(c => c.Prescriptions)
                    .ThenInclude(p => p.PrescriptionItems)
                .Include(c => c.Attachments)
                .Include(c => c.AIDiagnosisLogs)
                .FirstOrDefaultAsync(c => c.AppointmentId == appointmentId);
        }

       
        public async Task<ConsultationRecord?> GetForDiagnosisAnalysisAsync(Guid consultationId)
        {
            return await _dbSet
                .Include(c => c.Appointment)
                    .ThenInclude(a => a.Patient) 
                .Include(c => c.Prescriptions)
                    .ThenInclude(p => p.PrescriptionItems) 
                .Include(c => c.AIDiagnosisLogs) 
                .FirstOrDefaultAsync(c => c.Id == consultationId);
        }
    }
}