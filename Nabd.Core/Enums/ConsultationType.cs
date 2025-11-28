using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    // تحديد أنواع الكشوفات والزيارات لضبط الأسعار والجدولة
    public enum ConsultationType
    {
        // 1. القيمة الافتراضية
        Unknown = 0,

        // 2. الكشوفات المباشرة (Core Services)
        InitialVisit = 1,       // كشف أول (عادةً هو السعر الكامل)
        FollowUp = 2,           // زيارة متابعة (قد تكون مجانية أو بسعر مخفض خلال فترة محددة)

        // 3. الخدمات المتقدمة (Nabd - Digital/Specialty)
        Teleconsultation = 3,   // استشارة عن بعد (عبر الفيديو أو الهاتف)
        SecondOpinion = 4,      // طلب رأي ثانٍ
        Emergency = 5           // حالة طارئة وغير مجدولة
    }
}