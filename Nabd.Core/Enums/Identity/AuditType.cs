using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum AuditType
    {
        [Description("غير محدد")]
        None = 0,

        [Description("إضافة")]
        Create = 1,

        [Description("تعديل")]
        Update = 2,

        [Description("حذف")]
        Delete = 3,

        [Description("تسجيل دخول")]
        Login = 4,

        [Description("تسجيل خروج")]
        Logout = 5,

        [Description("عرض بيانات حساسة")]
        Access = 6,  // (زي ملف المريض النفسي)

        // --- إضافات Enterprise للأمان ---

        [Description("فشل تسجيل الدخول")]
        FailedLogin = 7, // (مهم لاكتشاف محاولات الاختراق Brute Force)

        [Description("تصدير بيانات")]
        Export = 8,      // (عشان لو دكتور سرق داتا المرضى وعمل Excel Export نعرفه)

        [Description("طباعة تقرير")]
        Print = 9        // (طباعة الروشتات أو التقارير)
    }
}