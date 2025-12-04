using Microsoft.AspNetCore.Identity;
using Nabd.Core.Entities.Identity; // RefreshToken
using Nabd.Core.Entities.Profiles; // Doctor, Patient
using Nabd.Core.Enums.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nabd.Core.Entities.Identity
{
    // ✅ التعديل: الوراثة من IdentityUser<Guid>
    public class AppUser : IdentityUser<Guid>
    {
        // ==========================================
        // 1. Identity Info
        // ==========================================
        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [NotMapped]
        public string DisplayName => $"{FirstName} {LastName}";

        public string? ProfilePictureUrl { get; set; }

        public DateTime LastLoginDate { get; set; } = DateTime.UtcNow;
        public string? LastLoginIp { get; set; }
        // ==========================================
        // 2. Auditing (نسخناهم هنا لأننا شلنا BaseEntity)
        // ==========================================
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft Delete
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // ==========================================
        // 3. System Role
        // ==========================================
        public required UserType UserType { get; set; }

        // ==========================================
        // 4. Relationships
        // ==========================================
        public Doctor? DoctorProfile { get; set; }
        public Patient? PatientProfile { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        // ملاحظة: خصائص مثل Email, UserName, PasswordHash, PhoneNumber
        // موجودة بالفعل داخل IdentityUser فمش محتاجين نكتبها تاني
    }
}