using Nabd.Core.Enums; // MedicationForm
using System;

namespace Nabd.Application.DTOs.Pharmacy
{
    /// <summary>
    /// يستخدم للبحث عن الأدوية وعرضها في القوائم
    /// </summary>
    public class MedicationDto
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Core Info (للعرض)
        // ==========================================
        public string TradeName { get; set; } = string.Empty; // "Panadol"
        public string ScientificName { get; set; } = string.Empty; // "Paracetamol" (مهم للـ AI)

        public string Strength { get; set; } = string.Empty; // "500mg"
        public string Form { get; set; } = string.Empty; // "Tablet" (from Enum Description)
        public string? Manufacturer { get; set; }

        // ==========================================
        // 2. UI Helpers (لتحسين تجربة المستخدم)
        // ==========================================

        // خاصية جاهزة للعرض في الـ Dropdown
        // مثال: "Panadol Extra - 500mg (Tablet)"
        public string DisplayName => $"{TradeName} - {Strength} ({Form})";

        // هل الدواء متاح/نشط؟
        public bool IsActive { get; set; }
    }
}