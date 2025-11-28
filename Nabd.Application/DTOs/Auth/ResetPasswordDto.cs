using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Auth
{
    // هذا DTO يُستخدم في الخطوة الأخيرة من عملية استعادة كلمة المرور
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Security Token/Code is required.")]
        // الرمز السري الذي تم إرساله على الإيميل (غالباً يكون Guid أو Base64)
        public required string ResetToken { get; set; }

        [Required(ErrorMessage = "New password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [DataType(DataType.Password)]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmation password is required.")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}