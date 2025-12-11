using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum AIDoctorAction
    {
        [Description("لم يتم اتخاذ إجراء")]
        NoAction = 0,    

        [Description("تم القبول")]
        Accepted = 1,    

        [Description("تم الرفض")]
        Rejected = 2,    

        [Description("تم التعديل")]
        Modified = 3     
    }
}