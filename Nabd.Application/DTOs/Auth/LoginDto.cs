using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Auth
{
    public class LoginDto
    {
        // ==================================================
        // 1. Email (اسم المستخدم)
        // ==================================================

        [Required(ErrorMessage = "The email address is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        // [MaxLength(100)] // يمكن إضافتها لتحديد الحد الأقصى لطول الإيميل
        public required string Email { get; set; }

        // ==================================================
        // 2. Password (كلمة المرور)
        // ==================================================

        [Required(ErrorMessage = "The password is required.")]
        [DataType(DataType.Password)]
        // [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public required string Password { get; set; }

        // ==================================================
        // 3. Optional: Remember Me (للتذكير)
        // ==================================================

        // يمكن إضافتها لاحقاً إذا كنا نحتاج للـ Persistence Cookies
        public bool RememberMe { get; set; } = false;

        // ==================================================
        // 4. Optional: Role (لتوجيه الـ Login في الـ API)
        // ==================================================

        // يمكن استخدامها لتوجيه طلب تسجيل الدخول إلى مسار معين
        // [JsonConverter(typeof(JsonStringEnumConverter))]
        // public string? UserRole { get; set; } 
    }
}