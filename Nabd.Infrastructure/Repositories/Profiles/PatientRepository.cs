using Microsoft.EntityFrameworkCore;
using Nabd.Core.DTOs;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Interfaces.Repositories.Profiles;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Profiles
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(NabdDbContext context) : base(context)
        {
        }

        // ==========================================
        // I. Core Retrieval
        // ==========================================

        public async Task<Patient?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.AppUser)
                .Include(p => p.MedicalHistoryItems.OrderByDescending(m => m.EventDate))
                .Include(p => p.MedicalAttachments)
                .Include(p => p.DoctorReviews)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Patient?> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(p => p.AppUser)
                .FirstOrDefaultAsync(p => p.AppUser.Email == email);
        }

        public async Task<IEnumerable<Patient>> GetPatientsWithMedicalHistoryAsync()
        {
            return await _dbSet
                .Include(p => p.MedicalHistoryItems)
                .AsNoTracking()
                .ToListAsync();
        }

        // ==========================================
        // II. Specialized & Optimized Queries
        // ==========================================

        public async Task<(IEnumerable<DoctorPatientDto> Patients, int TotalCount)> GetDoctorPatientsOptimizedAsync(
            Guid doctorId,
            int pageNumber,
            int pageSize)
        {
            var query = _dbSet
                .AsNoTracking()
                .Where(p => p.Appointments.Any(a => a.DoctorId == doctorId));

            var totalCount = await query.CountAsync();

            var patients = await query
                .OrderBy(p => p.FullName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new DoctorPatientDto
                {
                    Id = p.Id,
                    FirstName = p.AppUser.FirstName,
                    LastName = p.AppUser.LastName,
                    ProfileImageUrl = p.AppUser.ProfilePictureUrl, // ✅ الصح
                    PhoneNumber = p.PhoneNumber,
                    Gender = p.Gender.ToString(),
                    Age = p.Age,

                    // ❌ تم حذف ImageUrl من هنا لأنها مكررة وغير موجودة في DTO

                    City = p.City,
                    // Governorate = p.Governorate, // لو موجودة

                    TotalSessions = p.Appointments.Count(a => a.DoctorId == doctorId),

                    LastVisitDate = p.Appointments
                        .Where(a => a.DoctorId == doctorId)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Select(a => a.AppointmentDate)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return (patients, totalCount);
        }

        public async Task<Patient?> GetPatientWithLocationAsync(Guid patientId)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Id == patientId);
        }

        // ==========================================
        // III. Write Operations
        // ==========================================

        public async Task RemoveAsync(Patient patient, bool softDelete = true)
        {
            if (softDelete)
            {
                patient.IsDeleted = true;
                patient.DeletedAt = DateTime.UtcNow;
                _dbSet.Update(patient);
            }
            else
            {
                _dbSet.Remove(patient);
            }
        }
    }
}