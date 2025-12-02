using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum AIRequestType
    {
        [Description("مساعد التشخيص")]
        Diagnosis = 1,          // موديل التشخيص (Diagnosis Aid)

        [Description("مراجعة الروشتة")]
        PrescriptionCheck = 2,  // موديل مراجعة الروشتة (Prescription Analyzer)

        [Description("اقتراح تحاليل")]
        LabRecommendation = 3   // موديل اقتراح التحاليل الذكي
    }
}