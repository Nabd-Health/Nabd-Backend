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
                   .ThenInclude(d => d.ClinicBranches) 
                .Include(p => p.Patient)
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(Guid patientId)
        {
            return await _dbSet
                .Include(p => p.Doctor) 
                .Include(p => p.PrescriptionItems)
                    .ThenInclude(pi => pi.Medication)
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.CreatedAt) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(p => p.Patient) 
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