using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Feedback;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Profiles
{
    public class Patient : BaseEntity
    {
        // Identity Link
        public Guid AppUserId { get; set; }
        public required AppUser AppUser { get; set; }

        // Personal Info
        [Required, MaxLength(14)]
        public required string NationalId { get; set; }

        [Required, MaxLength(100)]
        public required string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public string? MaritalStatus { get; set; }

        [NotMapped]
        public int Age => DateTime.Now.Year - DateOfBirth.Year;

        // Medical Profile (AI Inputs)
        public BloodType BloodType { get; set; } = BloodType.Unknown;
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public string? ChronicDiseases { get; set; } // Text for AI
        public string? Allergies { get; set; }       // Text for AI

        public string? MedicalHistorySummary { get; set; } // NLP Summary

        // Emergency & Insurance
        [MaxLength(100)]
        public string? EmergencyContactName { get; set; }
        [Phone]
        public string? EmergencyContactPhone { get; set; }

        public bool HasInsurance { get; set; } = false;
        public string? InsuranceProvider { get; set; }
        public string? InsuranceNumber { get; set; }

        // Relationships
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // القوائم المنقولة من شريان (للتفاصيل والتقييم)
        public ICollection<MedicalHistoryItem> MedicalHistoryItems { get; set; } = new List<MedicalHistoryItem>();
        public ICollection<DoctorReview> DoctorReviews { get; set; } = new List<DoctorReview>();
        public ICollection<MedicalAttachment> MedicalAttachments { get; set; } = new List<MedicalAttachment>();
    }
}