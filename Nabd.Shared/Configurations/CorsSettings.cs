using System;

namespace Nabd.Shared.Configurations
{
    // إعدادات مشاركة الموارد عبر الأصول (CORS) لأمان المتصفح
    public class CorsSettings
    {
        // اسم السياسة التي سيتم تطبيقها في الـ Program.cs
        public string PolicyName { get; set; } = "NabdCorsPolicy"; // تم تعيين اسم افتراضي

        // النطاقات المسموح لها بالاتصال (مثال: http://localhost:3000)
        public string[] AllowedOrigins { get; set; } = Array.Empty<string>();

        // الطرق المسموحة (GET, POST, PUT, DELETE, etc.)
        public string[] AllowedMethods { get; set; } = Array.Empty<string>();

        // الهيدرز المسموحة (Content-Type, Authorization, etc.)
        public string[] AllowedHeaders { get; set; } = Array.Empty<string>();

        // هل يُسمح بإرسال الـ Cookies والـ Credentials؟ (مهم للـ Authentication)
        public bool AllowCredentials { get; set; } = true;
    }
}