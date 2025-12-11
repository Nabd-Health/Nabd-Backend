using Nabd.Application.DTOs.Medical;
using Nabd.Application.DTOs.Pharmacy; 
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; 
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Profiles
{
 
    public class PatientFullProfileDto
    {
        // ==========================================
        // 1. Personal & Demographic Data
        // ==========================================
        public Guid PatientId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }

  
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string? JobTitle { get; set; }
        public string? City { get; set; }

        // ==========================================
        // 2. Vital Statistics (AI Features)
        // ==========================================
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public double? Weight { get; set; }
        public double? Height { get; set; }

        // ==========================================
        // 3. Medical History (Timeline)
        // ==========================================


        public List<MedicalHistoryItemResponse> MedicalHistory { get; set; } = new();

        // ==========================================
        // 4. Clinical History 
        // ==========================================


        public List<PrescriptionResponse> RecentPrescriptions { get; set; } = new();

      
        public List<ConsultationRecordSummaryDto> PastVisits { get; set; } = new();
    }
}