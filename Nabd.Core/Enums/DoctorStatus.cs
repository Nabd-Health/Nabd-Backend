using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum DoctorStatus
    {
        Pending = 0,  // لسه مسجل جديد ومحتاج موافقة الأدمن
        Active = 1,   // شغال وزي الفل
        Suspended = 2, // موقوف (بسبب مشاكل أو عدم دفع اشتراك)
        OnVacation = 3 // في أجازة
    }
}