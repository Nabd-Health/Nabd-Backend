using Nabd.Core.Entities.Profiles; 
using Nabd.Core.Enums; 
using Nabd.Core.Enums.Medical; 
using Nabd.Core.Enums.Operations;
using Nabd.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nabd.Core.Interfaces.Repositories.Profiles
{
   
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        // ==========================================
        // I. Get By ID with Eager Loading
        // ==========================================

        
        Task<Doctor?> GetByIdWithDetailsAsync(Guid id);

      
        Task<Doctor?> GetByIdWithBranchesAsync(Guid id);

       
        Task<Doctor?> GetByIdWithSchedulesAsync(Guid id); 

        // ==========================================
        // II. Core Retrieval 
        // ==========================================

       
        Task<Doctor?> GetByEmailAsync(string email);

        
        Task<IEnumerable<Doctor>> GetVerifiedDoctorsAsync();

 
        Task<IEnumerable<Doctor>> GetBySpecialtyAsync(MedicalSpecialty specialty);

       
        Task<IEnumerable<Doctor>> GetDoctorsByGovernorateAsync(Governorate governorate);

        // ==========================================
        // III. Search & Filtering 
        // ==========================================

    
        Task<IEnumerable<Doctor>> SearchDoctorsAsync(
            string? searchTerm = null,
            MedicalSpecialty? specialty = null,
            Governorate? governorate = null,
            int? minYearsOfExperience = null,
            decimal? maxConsultationFee = null,
            double? minRating = null
        );

       
        Task<bool> IsAvailableAtAsync(Guid doctorId, DateTime dateTime);

        Task<IEnumerable<Doctor>> GetVerifiedDoctorsWithDetailsForListAsync();
    }
}