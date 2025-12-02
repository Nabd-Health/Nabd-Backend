using System.ComponentModel;

namespace Nabd.Core.Enums.Medical
{
    // تحديد أنواع الكشوفات والزيارات لضبط الأسعار والجدولة
    public enum ConsultationType
    {
        [Description("غير محدد")]
        Unknown = 0,

        // ==========================================
        // 1. الكشوفات الأساسية (Core Services)
        // ==========================================

        [Description("كشف جديد")]
        InitialVisit = 1,       // كشف أول (السعر الكامل)

        [Description("متابعة (استشارة)")]
        FollowUp = 2,           // زيارة متابعة (عادة مجانية أو مخفضة)

        // ==========================================
        // 2. الخدمات المتقدمة (Advanced Services)
        // ==========================================

        [Description("كشف عن بعد (أونلاين)")]
        Teleconsultation = 3,   // استشارة فيديو/صوت

        [Description("رأي طبي ثانٍ")]
        SecondOpinion = 4,      // مراجعة تشخيص دكتور آخر

        [Description("طوارئ")]
        Emergency = 5           // حالة عاجلة وغير مجدولة
    }
}