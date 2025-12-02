using System.ComponentModel;

namespace Nabd.Core.Enums.Operations
{
    public enum PaymentStatus
    {
        [Description("انتظار الدفع")]
        Pending = 0,

        [Description("تم الدفع")]
        Succeeded = 1,

        [Description("فشل الدفع")]
        Failed = 2,

        [Description("تم الاسترداد")]
        Refunded = 3,

        [Description("معفى من الرسوم")]
        Waived = 4
    }
}