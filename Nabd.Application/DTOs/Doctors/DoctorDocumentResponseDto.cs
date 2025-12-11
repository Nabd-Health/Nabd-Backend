using System;

namespace Nabd.Application.DTOs.Doctors
{
    public class DoctorDocumentResponseDto
    {
        public Guid Id { get; set; }

        
        public string DocumentType { get; set; } = string.Empty;


        public string FileUrl { get; set; } = string.Empty;

        // ==========================================
        // Status Info 
        // ==========================================
        public bool IsVerified { get; set; }

        public string? RejectionReason { get; set; } 

  
   
        public string Status => IsVerified
            ? "Verified"
            : (!string.IsNullOrEmpty(RejectionReason) ? "Rejected" : "Pending");

        public DateTime UploadedAt { get; set; }
    }
}