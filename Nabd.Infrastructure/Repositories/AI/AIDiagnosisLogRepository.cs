using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.AI;
using Nabd.Core.Interfaces.Repositories.AI;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.AI
{
    public class AIDiagnosisLogRepository : GenericRepository<AIDiagnosisLog>, IAIDiagnosisLogRepository
    {
        public AIDiagnosisLogRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AIDiagnosisLog>> GetByConsultationIdAsync(Guid consultationId)
        {
            return await _dbSet
                .Where(log => log.ConsultationRecordId == consultationId)
                .OrderByDescending(log => log.LoggedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<AIDiagnosisLog>> GetLogsForRetrainingAsync()
        {
            // نختار السجلات التي قام الطبيب بتقييمها (سواء صح أو خطأ)
            // لأننا نحتاج الـ Ground Truth (رأي الطبيب) للتدريب
            return await _dbSet
                .Where(log => log.WasCorrect.HasValue && log.DoctorCorrection != null)
                .OrderByDescending(log => log.LoggedAt)
                .ToListAsync();
        }

        public async Task<double> GetModelAccuracyPercentageAsync()
        {
            var totalEvaluatedLogs = await _dbSet.CountAsync(log => log.WasCorrect.HasValue);

            if (totalEvaluatedLogs == 0) return 0;

            var correctLogs = await _dbSet.CountAsync(log => log.WasCorrect == true);

            return (double)correctLogs / totalEvaluatedLogs * 100;
        }
    }
}