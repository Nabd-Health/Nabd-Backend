using System;

namespace Nabd.Application.DTOs.Operations
{
    public class ClinicBranchResponse
    {
        public Guid Id { get; set; }

        // ==========================================
        // 1. Basic Info
        // ==========================================
        public string Name { get; set; } = string.Empty; 
        public string PhoneNumber { get; set; } = string.Empty;
        public string? LandlineNumber { get; set; }

        // ==========================================
        // 2. Location (Flattened Address)
        // ==========================================

        public string Governorate { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string StreetAddress { get; set; } = string.Empty;

        //  (Google Maps)
        public string Address { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? GoogleMapLink { get; set; }

        // ==========================================
        // 3. Operational Info
        // ==========================================
     
        public decimal? CustomConsultationFee { get; set; }

        public bool IsActive { get; set; }
    }
}