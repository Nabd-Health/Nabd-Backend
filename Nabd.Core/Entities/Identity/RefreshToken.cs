using Nabd.Core.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Identity
{
    public class RefreshToken : BaseEntity
    {
        // ==========================================
        // 1. Linkage 
        // ==========================================
        public required Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; } = null!;

        // ==========================================
        // 2. Token Details 
        // ==========================================
        public required string Token { get; set; }

        public DateTime ExpiresOn { get; set; }

        public required string CreatedByIp { get; set; }


        // ==========================================
        // 3. Revocation
        // ==========================================
        public DateTime? RevokedOn { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReasonRevoked { get; set; }

        // ==========================================
        // 4. Security Rotation 
        // ==========================================
       
        public string? ReplacedByToken { get; set; }

        // ==========================================
        // 5. Computed Properties 
        // ==========================================
        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public bool IsRevoked => RevokedOn != null;

        public bool IsActive => !IsRevoked && !IsExpired;
    }
}