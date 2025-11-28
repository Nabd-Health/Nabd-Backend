using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum AdministrationRoute
    {
        Oral = 1,           // عن طريق الفم (أقراص/شراب)
        Injection = 2,      // حقن (IV, IM)
        Topical = 3,        // موضعي (مراهم/كريمات)
        Inhalation = 4,     // استنشاق (بخاخات)
        Sublingual = 5,     // تحت اللسان
        Rectal = 6          // عن طريق الشرج
    }
}
