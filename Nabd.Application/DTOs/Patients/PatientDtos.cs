using Nabd.Core.Enums;
using Nabd.Core.Enums.Medical;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Patients
{

    public class UpdatePatientProfileRequest
    {
        [Phone]
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? JobTitle { get; set; }
        public string? MaritalStatus { get; set; }

        public string? EmergencyContactName { get; set; }
        [Phone]
        public string? EmergencyContactPhone { get; set; }

  
        public bool HasInsurance { get; set; }
        public string? InsuranceProvider { get; set; }
        public string? InsuranceNumber { get; set; }
    }

    
    public class CreateMedicalHistoryItemRequest
    {
        [Required]
        public HistoryEventType Type { get; set; } 

        [Required]
        public string Title { get; set; } = string.Empty; 

        public string? Details { get; set; } 

        [Required]
        public DateTime EventDate { get; set; }

        public bool IsCritical { get; set; }
    }

    // 3. Response للتاريخ المرضي
    public class MedicalHistoryItemResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Details { get; set; }
        public DateTime EventDate { get; set; }
        public bool IsCritical { get; set; }
    }
}