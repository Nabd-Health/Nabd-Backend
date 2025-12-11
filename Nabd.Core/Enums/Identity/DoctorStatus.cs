using System.ComponentModel;

namespace Nabd.Core.Enums
{
    public enum DoctorStatus
    {
        [Description("في انتظار التفعيل")]
        Pending = 0,    

        [Description("نشط")]
        Active = 1,    

        [Description("موقوف إدارياً")]
        Suspended = 2,  

        [Description("في إجازة")]
        OnVacation = 3, 

        

        [Description("مرفوض")]
        Rejected = 4,  

        [Description("غير نشط")]
        Inactive = 5    
    }
}