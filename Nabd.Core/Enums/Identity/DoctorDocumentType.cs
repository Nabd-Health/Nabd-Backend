using System.ComponentModel;

namespace Nabd.Core.Enums.Identity
{
    public enum DoctorDocumentType
    {
        // Important for Verification (مستندات أساسية للتوثيق)
        [Description("البطاقة الشخصية")]
        NationalId = 1,

        [Description("رخصة مزاولة المهنة")]
        MedicalPracticeLicense = 2,

        [Description("عضوية النقابة")]
        SyndicateMembershipCard = 3,

        [Description("شهادة التخرج من كلية الطب")]
        MedicalGraduationCertificate = 4,

        [Description("شهادة التخصص")]
        SpecialtyCertificate = 5,

        // Optional Professional Information (مستندات إضافية للبروفايل)
        [Description("شهادات مهنية اضافية")]
        AdditionalCertificates = 6,

        [Description("جوائز وتقديرات")]
        AwardsAndRecognitions = 7,

        [Description("الأبحاث المنشورة")]
        PublishedResearch = 8,

        [Description("العضويات المهنية")]
        ProfessionalMemberships = 9
    }
}