using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    // تحديد الأدوار المتاحة داخل النظام
    public enum UserType
    {
        // ==========================================
        // 1. Safety Default
        // ==========================================
        Unknown = 0,        // قيمة افتراضية للسلامة (مستخدم غير معروف)

        // ==========================================
        // 2. Core Users (الأكثر استخداماً)
        // ==========================================
        Admin = 1,          // مدير النظام (يتحكم في كل شيء)
        Doctor = 2,         // مقدم الخدمة (الطبيب)
        Patient = 3,        // المستهلك (المريض)

        // ==========================================
        // 3. Enterprise & Verification Roles (للتوسع والمناقشة)
        // ==========================================
        Verifier = 4,       // مسؤول التوثيق (يراجع تراخيص الدكاترة - مهم للأمن)
        ExternalPartner = 5 // للصيدليات والمعامل (لربطهم في المستقبل بالـ E-Prescription)
    }
}