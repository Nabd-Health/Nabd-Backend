using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum AIRequestType
    {
        Diagnosis = 1,          // موديل التشخيص
        PrescriptionCheck = 2,  // موديل مراجعة الروشتة
        LabRecommendation = 3   // موديل اقتراح التحاليل
    }
}