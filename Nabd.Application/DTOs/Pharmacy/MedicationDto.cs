using Nabd.Core.Enums; 
using System;

namespace Nabd.Application.DTOs.Pharmacy
{
    
    public class MedicationDto
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Core Info
        // ==========================================
        public string TradeName { get; set; } = string.Empty; 
        public string ScientificName { get; set; } = string.Empty; 

        public string Strength { get; set; } = string.Empty; 
        public string Form { get; set; } = string.Empty; 
        public string? Manufacturer { get; set; }

        // ==========================================
        // 2. UI Helpers 
        // ==========================================

        public string DisplayName => $"{TradeName} - {Strength} ({Form})";

        public bool IsActive { get; set; }
    }
}