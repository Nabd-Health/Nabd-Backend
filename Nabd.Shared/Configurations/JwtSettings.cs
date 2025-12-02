using System;

namespace Nabd.Shared.Configurations
{
    // هذا الكلاس يمثل إعدادات JSON Web Token (JWT) اللازمة لعمليات التوقيع والتحقق
    public class JwtSettings
    {
        // المفتاح السري لتوقيع التوكنات (Secret Key for signing tokens)
        public string SecretKey { get; set; } = string.Empty;

        // الجهة المصدرة للتوكن (Issuer)
        public string Issuer { get; set; } = string.Empty;

        // الجمهور المستهدف (Audience)
        public string Audience { get; set; } = string.Empty;

        // مدة صلاحية توكن الدخول بالدقائق (قصيرة جداً للأمان)
        public int AccessTokenExpirationMinutes { get; set; } = 15;

        // مدة صلاحية توكن التجديد Refresh Token بالأيام (أطول للسماح للمستخدم بالبقاء مسجلاً)
        public int RefreshTokenExpirationDays { get; set; } = 7;

        // ==========================================
        // Validation Settings (إعدادات التحقق)
        // ==========================================

        public bool ValidateIssuer { get; set; } = true;
        public bool ValidateAudience { get; set; } = true;
        public bool ValidateLifetime { get; set; } = true; // تحقق من تاريخ الصلاحية
        public bool ValidateIssuerSigningKey { get; set; } = true; // تحقق من المفتاح السري

        // الانحراف الزمني المسموح به (لحل مشكلات اختلاف توقيت السيرفرات)
        public int ClockSkewMinutes { get; set; } = 5;
    }
}