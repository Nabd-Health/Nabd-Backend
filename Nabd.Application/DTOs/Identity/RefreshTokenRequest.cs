using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class RefreshTokenRequest
    {
        // التوكن الأصلي (حتى لو انتهت مدته)
        [Required(ErrorMessage = "Token is required")]
        public required string Token { get; set; }

        // توكن التجديد (الذي نستخدمه للحصول على توكن جديد)
        [Required(ErrorMessage = "RefreshToken is required")]
        public required string RefreshToken { get; set; }
    }
}