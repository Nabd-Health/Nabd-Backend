using System.ComponentModel;

namespace Nabd.Core.Enums.Medical
{
    public enum AdministrationRoute
    {
        [Description("عن طريق الفم")]
        Oral = 1,           

        [Description("حقن")]
        Injection = 2,      

        [Description("موضعي")]
        Topical = 3,       

        [Description("استنشاق")]
        Inhalation = 4,     

        [Description("تحت اللسان")]
        Sublingual = 5,

        [Description("شرجي")]
        Rectal = 6,

        [Description("قطرة")]
        Drops = 7          
    }
}