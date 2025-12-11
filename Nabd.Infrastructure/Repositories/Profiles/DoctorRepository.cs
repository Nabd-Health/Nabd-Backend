using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Profiles;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity; 
using Nabd.Core.Enums.Medical;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Profiles;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Profiles
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(NabdDbContext context) : base(context)
        {
        }

        // ==========================================
        // I. Get By ID with Details
        // ==========================================

        public async Task<Doctor?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.AppUser) 
                .Include(d => d.ClinicBranches)
                    .ThenInclude(b => b.Schedules) 
                .Include(d => d.VerificationDocuments) 
                .Include(d => d.DoctorReviews.OrderByDescending(r => r.CreatedAt).Take(5)) 
                .AsSplitQuery() 
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor?> GetByIdWithBranchesAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.ClinicBranches)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Doctor?> GetByIdWithSchedulesAsync(Guid id)
        {
            return await _dbSet
                .Include(d => d.ClinicBranches)
                    .ThenInclude(b => b.Schedules)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        // ==========================================
        // II. Core Retrieval
        // ==========================================

        public async Task<Doctor?> GetByEmailAsync(string email)
        {
            
            return await _dbSet
                .Include(d => d.AppUser)
                .FirstOrDefaultAsync(d => d.AppUser.Email == email);
        }

        public async Task<IEnumerable<Doctor>> GetVerifiedDoctorsAsync()
        {
            return await _dbSet
                .Include(d => d.AppUser)
                .Include(d => d.ClinicBranches)
                
                .Where(d => d.VerifiedAt != null && d.IsAvailable)
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty)
        {
            return await _dbSet
                .Include(d => d.AppUser)
                .Include(d => d.ClinicBranches)
                .Where(d => d.Specialization == specialty
                            && d.VerifiedAt != null
                            && d.IsAvailable)
                .OrderByDescending(d => d.AverageRating) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsByGovernorateAsync(Governorate governorate)
        {
            
            return await _dbSet
                .Include(d => d.ClinicBranches)
                .Where(d => d.ClinicBranches.Any(b => b.Governorate == governorate)
                            && d.VerifiedAt != null)
                .ToListAsync();
        }

        // ==========================================
        // III. Search & Filtering (The Search Engine)
        // ==========================================

        public async Task<IEnumerable<Doctor>> SearchDoctorsAsync(
            string? searchTerm = null,
            MedicalSpecialty? specialty = null,
            Governorate? governorate = null,
            int? minYearsOfExperience = null,
            decimal? maxConsultationFee = null,
            double? minRating = null)
        {
            // 1. Start with Base Query
            var query = _dbSet
                .Include(d => d.AppUser)
                .Include(d => d.ClinicBranches) 
                .Where(d => d.VerifiedAt != null && d.IsAvailable); 

            // 2. Apply Filters

            // Search Text (Name or Bio)
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var lowerTerm = searchTerm.ToLower();
                query = query.Where(d =>
                    d.FullName.ToLower().Contains(lowerTerm) ||
                    (d.Bio != null && d.Bio.ToLower().Contains(lowerTerm)));
            }

            // Specialty
            if (specialty.HasValue)
            {
                query = query.Where(d => d.Specialization == specialty.Value);
            }

            // Governorate (Check inside branches)
            if (governorate.HasValue)
            {
                query = query.Where(d => d.ClinicBranches.Any(b => b.Governorate == governorate.Value));
            }

            // Experience
            if (minYearsOfExperience.HasValue)
            {
                query = query.Where(d => d.YearsOfExperience >= minYearsOfExperience.Value);
            }

            // Fee (Check base fee OR if any branch matches the fee)
            if (maxConsultationFee.HasValue)
            {
                query = query.Where(d =>
                    d.ConsultationFee <= maxConsultationFee.Value ||
                    d.ClinicBranches.Any(b => b.CustomConsultationFee != null && b.CustomConsultationFee <= maxConsultationFee.Value));
            }

            // Rating (Direct SQL Filtering - Performance Win! 🚀)
            if (minRating.HasValue)
            {
                query = query.Where(d => d.AverageRating >= minRating.Value);
            }

            // 3. Execution
 
            return await query
                .OrderByDescending(d => d.AverageRating)
                .ThenBy(d => d.ConsultationFee)
                .ToListAsync();
        }

        public async Task<bool> IsAvailableAtAsync(Guid doctorId, DateTime dateTime)
        {
            var dayOfWeek = dateTime.DayOfWeek; 
            var timeOfDay = dateTime.TimeOfDay; 

         
            var isAvailable = await _context.DoctorSchedules
                .AnyAsync(s =>
                    s.DoctorId == doctorId &&
                    s.DayOfWeek == dayOfWeek &&
                    !s.IsDayOff &&
                    s.StartTime <= timeOfDay &&
                    s.EndTime >= timeOfDay
                );


            return isAvailable;
        }

        public async Task<IEnumerable<Doctor>> GetVerifiedDoctorsWithDetailsForListAsync()
        {
            return await _dbSet
                .Include(d => d.ClinicBranches) 
                .Where(d => d.VerifiedAt != null)
                .OrderByDescending(d => d.AverageRating) 
                .AsNoTracking() 
                .ToListAsync();
        }
    }
}