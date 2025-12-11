using System.ComponentModel;

namespace Nabd.Core.Enums.Operations
{
    public enum AppointmentStatus
    {
        [Description("في الانتظار")]
        Pending = 0, 

        [Description("محجوز ومؤكد")]
        Confirmed = 1,

        [Description("وصل المريض")]
        CheckedIn = 2, 

        [Description("الكشف جاري")]
        InProgress = 3, 

        [Description("انتهى الموعد")]
        Completed = 4,

        [Description("لم يحضر المريض")]
        NoShow = 5,

        [Description("ملغي")]
        Cancelled = 6,

        [Description("الدفع المعلق")]
        PendingPayment = 7
    }
}