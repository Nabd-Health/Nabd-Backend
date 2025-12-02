using System.ComponentModel;

namespace Nabd.Core.Enums.Medical
{
    public enum AdministrationRoute
    {
        [Description("عن طريق الفم")]
        Oral = 1,           // (أقراص/شراب)

        [Description("حقن")]
        Injection = 2,      // (IV, IM, SC)

        [Description("موضعي")]
        Topical = 3,        // (مراهم/كريمات)

        [Description("استنشاق")]
        Inhalation = 4,     // (بخاخات)

        [Description("تحت اللسان")]
        Sublingual = 5,

        [Description("شرجي")]
        Rectal = 6,

        [Description("قطرة")]
        Drops = 7           // (عيون/أذن) - إضافة صغيرة مهمة
    }
}