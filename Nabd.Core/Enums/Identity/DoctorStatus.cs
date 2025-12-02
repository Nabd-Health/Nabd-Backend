using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum DoctorStatus
    {
        [Description("في انتظار التفعيل")]
        Pending = 0,    // سجل بس لسه الأدمن مراجعش ورقه

        [Description("نشط")]
        Active = 1,     // شغال ويظهر في البحث

        [Description("موقوف إدارياً")]
        Suspended = 2,  // الأدمن وقفه (مخالفة أو عدم دفع اشتراك)

        [Description("في إجازة")]
        OnVacation = 3, // الدكتور مش بيستقبل حجوزات مؤقتاً

        // --- إضافات ---

        [Description("مرفوض")]
        Rejected = 4,   // تم رفض طلب التسجيل (بيانات غير صحيحة)

        [Description("غير نشط")]
        Inactive = 5    // الدكتور عطل حسابه بنفسه (بس لسه موجود في السيستم)
    }
}