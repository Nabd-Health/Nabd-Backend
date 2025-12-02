using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Shared.Configurations
{
    // هذا الكلاس يمثل إعدادات خدمة البريد الإلكتروني (SMTP) وقواعد OTP
    public class EmailSettings
    {
        // ==========================================
        // SMTP Server Configuration (إعدادات الخادم)
        // ==========================================
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string SmtpUsername { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;

        // ==========================================
        // Sender Identity (هوية المُرسِل)
        // ==========================================
        public string FromName { get; set; } = "Nabd HealthCare"; // تم التحديث من Shuryan
        public string FromEmail { get; set; } = string.Empty;
        public string ApplicationBaseUrl { get; set; } = string.Empty;

        // ==========================================
        // OTP & Security Rules (قواعد الأمان)
        // ==========================================

        // صلاحية كود تفعيل الإيميل
        public int VerificationOtpExpirationMinutes { get; set; } = 10;

        // صلاحية كود إعادة تعيين كلمة المرور
        public int PasswordResetOtpExpirationMinutes { get; set; } = 15;

        // طول كود OTP (مثال: 6 أرقام)
        public int OtpLength { get; set; } = 6;

        // الحد الأقصى لإعادة إرسال الكود في الساعة الواحدة (لمنع إساءة الاستخدام)
        public int MaxOtpResendAttemptsPerHour { get; set; } = 5;
    }
}