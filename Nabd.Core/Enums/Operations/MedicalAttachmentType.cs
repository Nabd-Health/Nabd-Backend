using System.ComponentModel;

namespace Nabd.Core.Enums.Operations
{
    public enum MedicalAttachmentType
    {
        [Description("مستند عام")]
        General = 0,

        [Description("نتيجة تحليل")]
        LabResult = 1,

        [Description("أشعة")]
        Radiology = 2,      // (Computer Vision Target)

        [Description("روشتة خارجية")]
        Prescription = 3,

        [Description("تقرير طبي")]
        MedicalReport = 4,

        [Description("بطاقة تأمين")]
        InsuranceCard = 5
    }
}