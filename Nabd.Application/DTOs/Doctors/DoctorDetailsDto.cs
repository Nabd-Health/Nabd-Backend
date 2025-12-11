using System;
using System.Collections.Generic;

namespace Nabd.Application.DTOs.Doctors
{
    public class DoctorDetailsDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public decimal BaseConsultationFee { get; set; }
        public int SessionDurationMinutes { get; set; }

        public List<ClinicBranchDto> Branches { get; set; } = new();
        public List<ReviewDto> RecentReviews { get; set; } = new();
    }

    public class ClinicBranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? GoogleMapLink { get; set; }
        public decimal Fee { get; set; }
        public bool IsActive { get; set; }
    }

    public class ReviewDto
    {
        public string PatientName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}