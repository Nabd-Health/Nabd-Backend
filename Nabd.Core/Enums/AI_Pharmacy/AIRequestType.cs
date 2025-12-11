using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum AIRequestType
    {
        [Description("مساعد التشخيص")]
        Diagnosis = 1,         

        [Description("مراجعة الروشتة")]
        PrescriptionCheck = 2,  

        [Description("اقتراح تحاليل")]
        LabRecommendation = 3   
    }
}