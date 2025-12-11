using System;

namespace Nabd.Application.DTOs.Pharmacy
{

    public class PrescriptionItemDto
    {
        public Guid Id { get; set; }
        public Guid MedicationId { get; set; }

        // ==========================================
        // 1. Medication Details (بيانات الدواء)
        // ==========================================
  
        public string MedicationName { get; set; } = string.Empty; 
        public string ScientificName { get; set; } = string.Empty;
        public string Strength { get; set; } = string.Empty; 
        public string Form { get; set; } = string.Empty; 

        // ==========================================
        // 2. Dosing Instructions 
        // ==========================================
        public string Dosage { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty; 
        public string? Duration { get; set; } 
        public string? Instructions { get; set; }

        public string? Notes { get; set; }
    }
}