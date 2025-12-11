using System;

namespace Nabd.Application.DTOs.Operations
{
    public class AppointmentListItemDto
    {
        public Guid Id { get; set; }

        // ============================
        // 1. Patient Info 
        // ============================
        public Guid PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public string? PatientPhoneNumber { get; set; }
        public string? PatientProfileImageUrl { get; set; } 

        // ============================
        // 2. Timing
        // ============================
        public DateTime AppointmentDate { get; set; }

        
        public int DurationMinutes { get; set; }

        // ============================
        // 3. Context & Status
        // ============================
        public string AppointmentType { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty; 

        public string? ReasonForVisit { get; set; } 

        // ============================
        // 4. Financials
        // ============================
        public decimal Price { get; set; }
    }
}