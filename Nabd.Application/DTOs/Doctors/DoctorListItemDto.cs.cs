using System;

namespace Nabd.Application.DTOs.Doctors
{
    public class DoctorListItemDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public decimal ConsultationFee { get; set; }
        public string PrimaryLocation { get; set; } = string.Empty;
        public bool IsAvailableToday { get; set; }
    }
}