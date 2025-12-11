using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Operations;
using Nabd.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories.Operations
{
    public class ClinicBranchRepository : GenericRepository<ClinicBranch>, IClinicBranchRepository
    {
        public ClinicBranchRepository(NabdDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ClinicBranch>> GetBranchesByDoctorIdAsync(Guid doctorId)
        {
            return await _dbSet
                .Include(b => b.Schedules) 
                .Where(b => b.DoctorId == doctorId)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClinicBranch>> GetBranchesNearLocationAsync(double latitude, double longitude, double radiusInKm)
        {
         

            var branches = await _dbSet
                .Include(b => b.Doctor) 
                .Where(b => b.IsActive && b.Latitude != null && b.Longitude != null)
                .ToListAsync();

          
            return branches
                .Where(b => CalculateDistance(latitude, longitude, b.Latitude!.Value, b.Longitude!.Value) <= radiusInKm)
                .ToList();
        }

        public async Task<IEnumerable<ClinicBranch>> GetBranchesByGovernorateAsync(Governorate governorate)
        {
            return await _dbSet
                .Include(b => b.Doctor)
                    .ThenInclude(d => d.AppUser) 
                .Where(b => b.IsActive && b.Governorate == governorate)
                .ToListAsync();
        }

        public async Task<ClinicBranch?> GetBranchWithAllDetailsAsync(Guid clinicBranchId)
        {
            return await _dbSet
                .Include(b => b.Doctor)
                    .ThenInclude(d => d.AppUser) 
                .Include(b => b.Schedules.OrderBy(s => s.DayOfWeek)) 
                .FirstOrDefaultAsync(b => b.Id == clinicBranchId);
        }

        // =========================================================
        // Private Helper: Haversine Formula 
        // =========================================================
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; 
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return distance;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
    }
}