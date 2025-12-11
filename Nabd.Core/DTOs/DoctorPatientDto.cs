using System;

namespace Nabd.Core.DTOs
{
    public class DoctorPatientDto
    {
        public Guid Id { get; set; }

        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}"; 

        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        public string? ProfileImageUrl { get; set; }

        
        public string? City { get; set; }
        public string? Governorate { get; set; }

      
        public DateTime? LastVisitDate { get; set; }
        public int TotalSessions { get; set; }      
        public decimal? Rating { get; set; }         
    }
}