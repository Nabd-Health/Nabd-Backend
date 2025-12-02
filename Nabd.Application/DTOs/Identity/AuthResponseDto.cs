using System;
using System.Collections.Generic;
using System.Text.Json.Serialization; // مهم عشان الـ Frontend يفهم الـ JSON صح

namespace Nabd.Application.DTOs.Identity
{
    public class AuthResponseDto
    {
        // 1. حالة العملية (عشان الـ Frontend يعرف يوجه المستخدم)
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }

        // 2. المفاتيح الأمنية (The Keys)
        public string? Token { get; set; } // Access Token (JWT)

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? RefreshToken { get; set; } // بيتبعت بس لو العملية نجحت

        public DateTime? RefreshTokenExpiration { get; set; } // اختياري: عشان الفرونت يعرف هيخلص امتى

        // 3. بيانات المستخدم (عشان الـ UI State)
        // بنرجعها هنا عشان نوفر Request إضافي لجلب البروفايل بعد الدخول
        public Guid UserId { get; set; }
        public string? DisplayName { get; set; } // "Dr. Seif" or "Ahmed Mohamed"
        public string? Email { get; set; }
        public string? UserRole { get; set; } // "Doctor" or "Patient" (مهم للـ Routing)

        // هل الحساب مفعل؟ (لو عايز توجهه لصفحة Verify Email)
        public bool IsEmailConfirmed { get; set; }
    }
}