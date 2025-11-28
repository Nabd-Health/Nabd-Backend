using Nabd.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities
{
    public class MedicalAttachment : BaseEntity
    {
        // ==========================================
        // 1. Linkage (التبعية - الملف ده يخص مين؟)
        // ==========================================

        public Guid PatientId { get; set; }
        public required Patient Patient { get; set; }

        // اختياري: ممكن الملف يترفع قبل الكشف، وبعدين نربطه بيه
        public Guid? ConsultationRecordId { get; set; }
        public ConsultationRecord? ConsultationRecord { get; set; }

        // مين اللي رفع الملف؟ (الدكتور ولا المريض ولا المعمل؟)
        // بنسجل الـ UserId عشان الـ Security Audit
        public required string UploadedByUserId { get; set; }

        // ==========================================
        // 2. File Metadata (بيانات الملف التقنية)
        // ==========================================

        [Required]
        [MaxLength(250)]
        public required string FileName { get; set; } // الاسم الأصلي للملف

        [Required]
        public required string FileUrl { get; set; } // الرابط على السيرفر (Cloudinary/Azure Blob/Local)

        [MaxLength(50)]
        public required string FileType { get; set; } // الامتداد (.pdf, .jpg, .dicom)

        public long FileSizeInBytes { get; set; } // الحجم (عشان نمنع الملفات العملاقة)

        // ==========================================
        // 3. Medical Context (التصنيف الطبي)
        // ==========================================

        // نوع المرفق: أشعة، تحليل، روشتة خارجية
        public MedicalAttachmentType AttachmentType { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } // وصف (مثال: "أشعة على الصدر قبل العملية")

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // ==========================================
        // 4. AI Computer Vision Integration (المستقبل)
        // ==========================================

        // هل تم تحليل الملف ده بواسطة الـ AI؟
        public bool IsAnalyzedByAI { get; set; } = false;

        // نتيجة التحليل (JSON)
        // مثال: { "detected": "Fracture", "location": "Left Arm", "confidence": 0.95 }
        public string? AIAnalysisResultJson { get; set; }

        // لو الـ AI طلع تقرير نصي (Summary)
        public string? AISummary { get; set; }
    }
}