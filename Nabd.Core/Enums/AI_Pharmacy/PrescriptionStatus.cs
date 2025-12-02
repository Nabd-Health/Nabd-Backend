using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum PrescriptionStatus
    {
        [Description("نشطة (لم تصرف)")]
        Active = 1,

        [Description("تم صرفها")]
        Dispensed = 2,

        [Description("منتهية الصلاحية")]
        Expired = 3,

        [Description("ملغاة")]
        Cancelled = 4,

        [Description("صرف جزئي")]
        PartiallyDispensed = 5  // (إضافة Enterprise: لو الصيدلية معندهاش كل الأدوية)
    }
}