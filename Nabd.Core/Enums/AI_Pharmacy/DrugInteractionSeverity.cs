using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum DrugInteractionSeverity
    {
        [Description("لا يوجد تفاعل")]
        None = 0,

        [Description("تفاعل طفيف (Minor)")]
        Minor = 1,      

        [Description("تفاعل متوسط (Moderate)")]
        Moderate = 2,  

        [Description("تفاعل خطير (Major)")]
        Major = 3,      

        [Description("ممنوع الاستخدام (Contraindicated)")]
        Contraindicated = 4 
    }
}