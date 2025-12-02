using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum MedicationForm
    {
        [Description("أقراص")]
        Tablet = 1,

        [Description("كبسول")]
        Capsule = 2,

        [Description("شراب")]
        Syrup = 3,

        [Description("حقن")]
        Injection = 4,

        [Description("مرهم / كريم")]
        Ointment = 5,

        [Description("قطرة")]
        Drops = 6,

        [Description("بخاخ")]
        Inhaler = 7,

        [Description("فوار / أكياس")]
        Sachet = 8,

        [Description("لبوس")]
        Suppository = 9,

        [Description("محلول")]
        Solution = 10
    }
}