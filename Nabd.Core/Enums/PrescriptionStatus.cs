using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum PrescriptionStatus
    {
        Active = 1,     // سارية ولم تصرف بعد
        Dispensed = 2,  // تم صرفها من الصيدلية
        Expired = 3,    // انتهت صلاحيتها
        Cancelled = 4   // ألغاها الطبيب
    }
}