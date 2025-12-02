using System.ComponentModel;

namespace Nabd.Core.Enums.Operations
{
    public enum AppointmentType
    {
        [Description("زيارة عيادة")]
        ClinicVisit = 0,

        [Description("كشف أونلاين")]
        VideoCall = 1,   // Telemedicine

        [Description("استشارة / متابعة")]
        FollowUp = 2,

        [Description("حالة طارئة")]
        Urgent = 3
    }
}