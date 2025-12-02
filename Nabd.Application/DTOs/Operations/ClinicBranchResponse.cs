using System;

namespace Nabd.Application.DTOs.Operations
{
    public class ClinicBranchResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Basic Info
        // ==========================================
        public string Name { get; set; } = string.Empty; // "عيادة المهندسين"
        public string PhoneNumber { get; set; } = string.Empty;
        public string? LandlineNumber { get; set; }

        // ==========================================
        // 2. Location (Flattened Address)
        // ==========================================
        // بنرجع المحافظة كـ String عشان العرض في الـ Frontend
        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;

        // للمرائط (Google Maps)
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 3. Operational Info
        // ==========================================
        // هل الفرع ده ليه سعر كشف مختلف عن سعر الدكتور الأساسي؟
        public decimal? CustomConsultationFee { get; set; }

        public bool IsActive { get; set; }
    }
}