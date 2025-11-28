using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums;
using System.Text.Json.Serialization;

namespace Nabd.Application.DTOs.Auth
{
    // هذا الكلاس يمثل الإخراج (Response) بعد نجاح تسجيل الدخول أو التسجيل
    public class AuthResponseDto
    {
        // ==================================================
        // 1. Tokens (مفتاح الجلسة)
        // ==================================================

        [Required]
        // الـ JWT (التوكن القصير الأجل)
        public required string Token { get; set; }

        [Required]
        // الـ Refresh Token (التوكن الطويل الأجل للتجديد)
        public required string RefreshToken { get; set; }

        // ==================================================
        // 2. User/Identity Information (معلومات الحساب الأساسية)
        // ==================================================

        [Required]
        public required Guid UserId { get; set; } // الـ ID الأساسي للمستخدم

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        // دور المستخدم (Doctor, Patient, Admin) - يستخدم في الـ Frontend للتوجيه
        public required string Role { get; set; }

        // ==================================================
        // 3. Optional Profile Details (لتحسين تجربة المستخدم وتقليل الـ API Calls)
        // ==================================================

        // الاسم الكامل للترحيب به في الـ Dashboard
        public string? FullName { get; set; }

        // مسار الصورة الشخصية (لو تم رفعها)
        public string? ProfilePictureUrl { get; set; }

        // رسالة مخصصة (مثال: "Welcome back, Doctor!")
        public string? Message { get; set; }
    }
}