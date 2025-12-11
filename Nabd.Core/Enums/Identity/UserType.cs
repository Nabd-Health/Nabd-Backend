using System.ComponentModel;

namespace Nabd.Core.Enums.Identity
{
    public enum UserType
    {
        [Description("غير محدد")]
        Unknown = 0,        

        [Description("مدير النظام")]
        Admin = 1,         

        [Description("طبيب")]
        Doctor = 2,         

        [Description("مريض")]
        Patient = 3,        

        [Description("فاحص الطبيب")]
        Verifier = 4        
    }
}