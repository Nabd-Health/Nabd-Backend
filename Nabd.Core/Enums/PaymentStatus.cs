using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,    // انتظار الدفع (مثال: الدفع عند الوصول)
        Succeeded = 1,  // تم الدفع بنجاح
        Failed = 2,     // فشل الدفع (مهم في الـ Audit Log)
        Refunded = 3,   // تم استرداد المبلغ
        Waived = 4      // تم الإعفاء من الرسوم (مجاني)
    }
}