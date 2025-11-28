using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum AuditType
    {
        None = 0,
        Create = 1, // إضافة سجل جديد
        Update = 2, // تعديل سجل موجود
        Delete = 3, // مسح سجل
        Login = 4,  // عملية تسجيل دخول
        Logout = 5, // تسجيل خروج
        Access = 6  // مجرد مشاهدة (لو بيانات حساسة زي ملف نفسي)
    }
}