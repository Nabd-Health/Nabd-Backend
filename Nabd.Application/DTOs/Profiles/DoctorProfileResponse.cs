using Nabd.Application.DTOs.Operations; 
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Profiles
{
   
    public class DoctorProfileResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Identity & Professional Info
        // ==========================================
        public string FullName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; } 

        public string Specialization { get; set; } = string.Empty; 
        public int YearsOfExperience { get; set; }

        // ==========================================
        // 2. Status & Quality 
        // ==========================================
        public bool IsVerified { get; set; } 
        public double AverageRating { get; set; } 
        public int TotalReviews { get; set; } 

        // ==========================================
        // 3. Operational Info
        // ==========================================
        public decimal ConsultationFee { get; set; } 
        public int SessionDurationMinutes { get; set; }


        public List<ClinicBranchResponse> Branches { get; set; } = new();
    }
}