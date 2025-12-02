using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum AIDoctorAction
    {
        [Description("لم يتم اتخاذ إجراء")]
        NoAction = 0,    // الدكتور طنش الاقتراح

        [Description("تم القبول")]
        Accepted = 1,    // الدكتور وافق على اقتراح الـ AI

        [Description("تم الرفض")]
        Rejected = 2,    // الدكتور رفض الاقتراح

        [Description("تم التعديل")]
        Modified = 3     // الدكتور قبل الاقتراح بس عدل عليه
    }
}