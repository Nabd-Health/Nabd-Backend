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
        Access = 6,  

        // ---  Enterprise  ---

        [Description("فشل تسجيل الدخول")]
        FailedLogin = 7, 

        [Description("تصدير بيانات")]
        Export = 8,      

        [Description("طباعة تقرير")]
        Print = 9        
    }
}