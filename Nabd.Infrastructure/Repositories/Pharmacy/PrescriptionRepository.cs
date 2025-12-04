using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Interfaces.Repositories.Pharmacy;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Pharmacy
{
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<Prescription?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Doctor)
                   .ThenInclude(d => d.ClinicBranches) // عشان نطبع عنوان العيادة في الروشتة
                .Include(p => p.Patient)
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication) // تفاصيل الدواء (الاسم، التركيز)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Include(p => p.Doctor) // مين الدكتور اللي كتبها؟
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication)
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.CreatedAt) // الأحدث أولاً
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(p => p.Patient) // الروشتة دي لمين؟
                .Where(p => p.DoctorId == doctorId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<Prescription?> GetByConsultationIdAsync(Guid consultationId)
        {
            return await _dbSet
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication)
                .FirstOrDefaultAsync(p => p.ConsultationRecordId == consultationId);
        }

        public async Task<IEnumerable<Prescription>> GetPrescriptionsContainingMedicationAsync(Guid medicationId)
        {
            // بنستخدم Any عشان ندخل جوا الـ List بتاعة الـ Items
            return await _dbSet
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication)
                .Where(p => p.PrescriptionItems.Any(pi => pi.MedicationId == medicationId))
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}