using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum AIDoctorAction
    {
        NoAction = 0,    // الدكتور طنش الاقتراح
        Accepted = 1,    // الدكتور اختار اقتراح الـ AI كما هو (Good Job AI)
        Rejected = 2,    // الدكتور رفض الاقتراح واختار حاجة تانية
        Modified = 3     // الدكتور قبل الاقتراح بس عدل عليه شوية
    }
}