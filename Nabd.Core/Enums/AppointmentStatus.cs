using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum AppointmentStatus
    {
        Pending = 0,    // حجز مبدئي لسه الدكتور مأكدش
        Confirmed = 1,  // الدكتور وافق (أو تأكيد أوتوماتيك)
        Completed = 2,  // الكشف تم بنجاح
        Cancelled = 3,  // اتلغى
        NoShow = 4,     // المريض مجاش (مهم عشان الـ Blacklist)
        InProgress = 5  // المريض داخل الغرفة دلوقتي
    }
}