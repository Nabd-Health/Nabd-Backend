using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Identity
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب.")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صالحة.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة.")]
        public required string Password { get; set; }
    }
}