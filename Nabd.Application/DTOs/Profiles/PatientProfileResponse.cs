using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical; 
using System;

namespace Nabd.Application.DTOs.Profiles
{
  
    public class PatientProfileResponse
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; } 

        // ==========================================
        // 1. Identity & Contact
        // ==========================================
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }

        // ==========================================
        // 2. Demographics 
        // ==========================================
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; } 
        public Gender Gender { get; set; }

        public string? City { get; set; }
        public string? Address { get; set; }
        public string? JobTitle { get; set; }
        public string? MaritalStatus { get; set; }

        // ==========================================
        // 3. Medical Baseline 
        // ==========================================
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public double? Weight { get; set; }
        public double? Height { get; set; }

        // ==========================================
        // 4. Safety & Emergency
        // ==========================================
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }
}