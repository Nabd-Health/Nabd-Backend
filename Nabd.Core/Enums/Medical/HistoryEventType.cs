using System.ComponentModel;

namespace Nabd.Core.Enums.Medical
{
    // أنواع السجلات التي تظهر في الخط الزمني للمريض
    public enum HistoryEventType
    {
        [Description("كشف طبي")]
        Consultation = 1,

        [Description("نتيجة تحليل")]
        LabResult = 2,

        [Description("أشعة")]
        Radiology = 3,

        [Description("دخول مستشفى")]
        Admission = 4,

        [Description("تطعيم")]
        Vaccination = 5,

        [Description("تنبيه طبي")]
        Alert = 6,          // (حساسية أو مرض مزمن)

        [Description("عملية جراحية")]
        Surgery = 7         // (إضافة Enterprise)
    }
}