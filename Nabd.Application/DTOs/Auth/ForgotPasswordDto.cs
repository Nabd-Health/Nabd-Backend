using System.ComponentModel.DataAnnotations;

namespace Nabd.Application.DTOs.Auth
{
    // هذا DTO يُستخدم في الخطوة الأولى من عملية استعادة كلمة المرور
    // يقوم بجمع الإيميل لإرسال رابط أو كود إعادة التعيين عليه.
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public required string Email { get; set; }
    }
}