using System.ComponentModel;

namespace Nabd.Core.Enums.Identity
{
    public enum UserType
    {
        [Description("غير محدد")]
        Unknown = 0,        // قيمة افتراضية للأمان

        [Description("مدير النظام")]
        Admin = 1,          // Admin

        [Description("طبيب")]
        Doctor = 2,         // Service Provider

        [Description("مريض")]
        Patient = 3,        // Consumer

        [Description("فاحص الطبيب")]
        Verifier = 4        // لمراجعة التراخيص (Enterprise Feature)
    }
}