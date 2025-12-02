using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum DrugInteractionSeverity
    {
        [Description("لا يوجد تفاعل")]
        None = 0,

        [Description("تفاعل طفيف (Minor)")]
        Minor = 1,      // لا يحتاج تغيير الدواء، فقط مراقبة

        [Description("تفاعل متوسط (Moderate)")]
        Moderate = 2,   // يحتاج حذر أو تعديل الجرعة

        [Description("تفاعل خطير (Major)")]
        Major = 3,      // يجب تغيير الدواء فوراً

        [Description("ممنوع الاستخدام (Contraindicated)")]
        Contraindicated = 4 // خطر على الحياة
    }
}