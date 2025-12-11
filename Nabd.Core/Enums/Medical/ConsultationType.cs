using System.ComponentModel;

namespace Nabd.Core.Enums.Medical
{
  
    public enum ConsultationType
    {
        [Description("غير محدد")]
        Unknown = 0,

        // ==========================================
        // 1.  (Core Services)
        // ==========================================

        [Description("كشف جديد")]
        InitialVisit = 1,      

        [Description("متابعة (استشارة)")]
        FollowUp = 2,          

        // ==========================================
        // 2.  (Advanced Services)
        // ==========================================

        [Description("كشف عن بعد (أونلاين)")]
        Teleconsultation = 3,   

        [Description("رأي طبي ثانٍ")]
        SecondOpinion = 4,      
        [Description("طوارئ")]
        Emergency = 5           
    }
}