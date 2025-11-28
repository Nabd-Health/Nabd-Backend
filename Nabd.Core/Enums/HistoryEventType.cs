using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    // أنواع السجلات التي تظهر في الخط الزمني
    public enum HistoryEventType
    {
        Consultation = 1,  // كشف أو استشارة
        LabResult = 2,     // نتيجة تحليل دم أو غيره
        Radiology = 3,     // أشعة أو MRI
        Admission = 4,     // دخول أو خروج من مستشفى (Hospital Stay)
        Vaccination = 5,   // تطعيم
        Alert = 6          // تنبيه حساسية أو مرض مزمن مُسجل حديثاً
    }
}