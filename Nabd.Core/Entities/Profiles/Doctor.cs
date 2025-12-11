using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Feedback;
using Nabd.Core.Entities.Identity;
using Nabd.Core.Entities.Medical;
using Nabd.Core.Entities.Operations;
using Nabd.Core.Entities.Pharmacy;
using Nabd.Core.Enums;
using Nabd.Core.Enums.Identity;
using Nabd.Core.Enums.Medical;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Profiles
{
    public class Doctor : BaseEntity
    {
        // ==========================================
        // 1. Identity Link 
        // ==========================================
        public Guid AppUserId { get; set; }
        public required AppUser AppUser { get; set; }

        // ==========================================
        // 2. Personal Info 
        // ==========================================
        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; } 

        public string? ProfilePictureUrl { get; set; }

        // ==========================================
        // 3. Professional Info 
        // ==========================================

        [Required]
      
        public MedicalSpecialty Specialization { get; set; }

        [MaxLength(1500)] 
        public string? Bio { get; set; }

        public string? MedicalLicenseNumber { get; set; } 
        public string? GraduationUniversity { get; set; }
        public int YearsOfExperience { get; set; }

        // ==========================================
        // 4. Clinic & Financials
        // ==========================================

        [Column(TypeName = "decimal(18,2)")]
        public decimal ConsultationFee { get; set; }

        public int SessionDurationMinutes { get; set; } = 30;

        // ==========================================
        // 5. System Status & Verification
        // ==========================================

        public DoctorStatus Status { get; set; } = DoctorStatus.Pending;
        public bool IsAvailable { get; set; } = true;

    
        public DateTime? VerifiedAt { get; set; }

        // ==========================================
        // 6. Statistics
        // ==========================================
        public double AverageRating { get; set; } = 0.0;
        public int TotalReviews { get; set; } = 0;

        // ==========================================
        // 7. Relationships
        // ==========================================

    
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

       
        public ICollection<ClinicBranch> ClinicBranches { get; set; } = new List<ClinicBranch>();


        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();

 
        public ICollection<DoctorReview> DoctorReviews { get; set; } = new List<DoctorReview>();

       
        public ICollection<DoctorDocument> VerificationDocuments { get; set; } = new List<DoctorDocument>();
    }
}