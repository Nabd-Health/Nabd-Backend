using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Nabd.Core.Enums
{
    public enum AppointmentType
    {
        ClinicVisit = 0, // كشف عادي في العيادة
        VideoCall = 1,   // كشف أونلاين (Telemedicine)
        FollowUp = 2,    // استشارة (ممكن تكون بسعر مخفض)
        Urgent = 3       // حالة طارئة
    }
}