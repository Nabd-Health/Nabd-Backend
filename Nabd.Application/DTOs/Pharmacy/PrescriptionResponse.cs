using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Pharmacy
{
    public class PrescriptionResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Identification 
        // ==========================================
        public string UniqueCode { get; set; } = string.Empty; 
        public DateTime IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Status { get; set; } = string.Empty; 

        // ==========================================
        // 2. Context 
        // ==========================================
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public Guid ConsultationRecordId { get; set; } 

        // ==========================================
        // 3. Clinical Content 
        // ==========================================
        public string? Notes { get; set; } 

   
        public List<PrescriptionItemDto> Items { get; set; } = new();

        // ==========================================
        // 4. AI & Safety 
        // ==========================================
        public bool IsReviewedByAI { get; set; } 
        public bool HasSafetyAlerts { get; set; } 
    }
}