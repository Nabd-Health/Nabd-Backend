using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class VerifyEmailRequest
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "كود التفعيل مطلوب.")]
        public required string Token { get; set; }
    }
}