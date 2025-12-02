using System;

namespace Nabd.Application.DTOs.Pharmacy
{
    /// <summary>
    /// يمثل صنف دواء واحد داخل الروشتة (للعرض)
    /// </summary>
    public class PrescriptionItemDto
    {
        public Guid Id { get; set; }
        public Guid MedicationId { get; set; }

        // ==========================================
        // 1. Medication Details (بيانات الدواء)
        // ==========================================
        // (تم دمج بيانات الدواء هنا لتسهيل العرض دون الحاجة لـ Join)
        public string MedicationName { get; set; } = string.Empty; // Trade Name (Panadol)
        public string ScientificName { get; set; } = string.Empty; // (Paracetamol)
        public string Strength { get; set; } = string.Empty; // (500mg)
        public string Form { get; set; } = string.Empty; // (Tablet)

        // ==========================================
        // 2. Dosing Instructions (التعليمات)
        // ==========================================
        public string Dosage { get; set; } = string.Empty; // (2 tablets)
        public string Frequency { get; set; } = string.Empty; // (Every 8 hours)
        public string? Duration { get; set; } // (For 5 days)
        public string? Instructions { get; set; } // (After meals)

        public string? Notes { get; set; }
    }
}