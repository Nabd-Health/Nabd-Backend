using System;

namespace Nabd.Application.DTOs.Operations
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Timing & Schedule 
        // ==========================================
        public DateTime AppointmentDate { get; set; }
        public int DurationMinutes { get; set; }

      
        public DateTime? ActualArrivalDate { get; set; }

        // ==========================================
        // 2. Status & Type 
        // ==========================================
        public string Status { get; set; } = string.Empty; 
        public string Type { get; set; } = string.Empty;   


        public string? CancellationReason { get; set; }

        // ==========================================
        // 3. Patient Info 
        // ==========================================
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? PatientPhoneNumber { get; set; }
        public string? PatientProfileImageUrl { get; set; }
        public int? PatientAge { get; set; } 
        public string? PatientGender { get; set; }

        // ==========================================
        // 4. Doctor Info 
        // ==========================================
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;
        public string? ClinicBranchName { get; set; } 

        // ==========================================
        // 5. Clinical Context 
        // ==========================================
        public string? ReasonForVisit { get; set; } 

       
        public Guid? ConsultationRecordId { get; set; }
        public bool HasPrescription { get; set; } 

        // ==========================================
        // 6. Financials 
        // ==========================================
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}