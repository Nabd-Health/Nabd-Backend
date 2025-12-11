using Nabd.Application.DTOs.Pharmacy; 
using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Medical
{
    public class ConsultationRecordResponse
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime Date { get; set; }

        // ==========================================
        // 1. Context 
        // ==========================================
        public string DoctorName { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;

        // ==========================================
        // 2. Subjective (الشكوى والأعراض - AI Input)
        // ==========================================
        public string ChiefComplaint { get; set; } = string.Empty; 
        public string Symptoms { get; set; } = string.Empty;      
        public string? HistoryOfPresentIllness { get; set; }

        // ==========================================
        // 3. Objective (AI Features)
        // ==========================================

        public double? Temperature { get; set; }
        public string? BloodPressure { get; set; } 
        public int? HeartRate { get; set; }
        public int? RespiratoryRate { get; set; }
        public double? OxygenSaturation { get; set; }
        public double? Weight { get; set; }

        public string? PhysicalExaminationNotes { get; set; }

        // ==========================================
        // 4. Assessment
        // ==========================================
        public string FinalDiagnosis { get; set; } = string.Empty;
        public string? ProvisionalDiagnosis { get; set; }


        public bool WasAIAssisted { get; set; }

        // ==========================================
        // 5. Plan
        // ==========================================
        public string? TreatmentPlan { get; set; } 
        public DateTime? RecommendedFollowUpDate { get; set; }


        public List<PrescriptionResponse> Prescriptions { get; set; } = new();


        public List<string> AttachmentUrls { get; set; } = new();
    }
}