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
        // SMTP Server Configuration 
        // ==========================================
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string SmtpUsername { get; set; } = string.Empty;
        public string SmtpPassword { get; set; } = string.Empty;

        // ==========================================
        // Sender Identity 
        // ==========================================
        public string FromName { get; set; } = "Nabd HealthCare"; 
        public string FromEmail { get; set; } = string.Empty;
        public string ApplicationBaseUrl { get; set; } = string.Empty;

        // ==========================================
        // OTP & Security Rules 
        // ==========================================

       
        public int VerificationOtpExpirationMinutes { get; set; } = 10;

        
        public int PasswordResetOtpExpirationMinutes { get; set; } = 15;

        
        public int OtpLength { get; set; } = 6;

       
        public int MaxOtpResendAttemptsPerHour { get; set; } = 5;
    }
}