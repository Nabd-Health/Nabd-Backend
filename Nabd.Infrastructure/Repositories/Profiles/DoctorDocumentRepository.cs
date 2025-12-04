using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Interfaces.Repositories.Profiles;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Profiles
{
    public class DoctorDocumentRepository : GenericRepository<DoctorDocument>, IDoctorDocumentRepository
    {
        public DoctorDocumentRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DoctorDocument>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Where(d => d.DoctorId == doctorId)
                .OrderByDescending(d => d.UploadedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorDocument>> GetPendingDocumentsAsync()
        {
            // "قيد الانتظار" تعني أنها لم توثق بعد، ولم يتم رفضها (لا يوجد سبب رفض)
            return await _dbSet
                .Include(d => d.Doctor)
                    .ThenInclude(doc => doc.AppUser) // عشان الأدمن يشوف اسم الدكتور صاحب الوثيقة
                .Where(d => !d.IsVerified && string.IsNullOrEmpty(d.RejectionReason))
                .OrderBy(d => d.UploadedAt) // الأقدم أولاً عشان نخلصهم بالدور
                .ToListAsync();
        }

        public async Task<IEnumerable<DoctorDocument>> GetByStatusAsync(VerificationDocumentStatus status)
        {
            var query = _dbSet
                .Include(d => d.Doctor)
                .ThenInclude(doc => doc.AppUser)
                .AsQueryable();

            // تحويل الـ Enum لشرط منطقي بناءً على حقول الـ Entity المتاحة
            switch (status)
            {
                case VerificationDocumentStatus.Accepted:
                    query = query.Where(d => d.IsVerified);
                    break;

                case VerificationDocumentStatus.Rejected:
                    // مرفوضة = غير موثقة + يوجد سبب رفض
                    query = query.Where(d => !d.IsVerified && !string.IsNullOrEmpty(d.RejectionReason));
                    break;

                case VerificationDocumentStatus.Pending:
                    // معلقة = غير موثقة + لا يوجد سبب رفض
                    query = query.Where(d => !d.IsVerified && string.IsNullOrEmpty(d.RejectionReason));
                    break;
            }

            return await query
                .OrderByDescending(d => d.UploadedAt)
                .ToListAsync();
        }
    }
}