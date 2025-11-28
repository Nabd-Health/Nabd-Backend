using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nabd.Core.Enums
{
    public enum MedicalAttachmentType
    {
        General = 0,        // مستند عام
        LabResult = 1,      // نتيجة تحليل (PDF/Image)
        Radiology = 2,      // أشعة (X-Ray, MRI, CT) -> ده بتاع الـ Computer Vision
        Prescription = 3,   // روشتة خارجية (صورة)
        MedicalReport = 4,  // تقرير خروج أو عملية
        InsuranceCard = 5   // صورة كارت التأمين
    }
}