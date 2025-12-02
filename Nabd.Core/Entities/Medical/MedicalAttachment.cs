using Nabd.Core.Entities.Base;
using Nabd.Core.Entities.Profiles; // Patient
using Nabd.Core.Enums.Operations;  // MedicalAttachmentType
using System.ComponentModel.DataAnnotations;

namespace Nabd.Core.Entities.Medical
{
    public class MedicalAttachment : BaseEntity
    {
        // ==========================================
        // 1. Linkage (التبعية)
        // ==========================================

        public Guid PatientId { get; set; }
        public virtual required Patient Patient { get; set; }

        // اختياري: ربطه بكشف معين (عشان الدكتور يعرف إن الأشعة دي طلبها في الكشف ده)
        public Guid? ConsultationRecordId { get; set; }
        public virtual ConsultationRecord? ConsultationRecord { get; set; }

        // [مهم للأمان]: مين اللي رفع الملف؟ (عشان لو اترفع ملف غلط نعرف نحاسب مين)
        public required string UploadedByUserId { get; set; }

        // ==========================================
        // 2. File Metadata (البيانات التقنية)
        // ==========================================

        [Required]
        [MaxLength(250)]
        public required string FileName { get; set; } // الاسم الأصلي (blood_test.pdf)

        [Required]
        public required string FileUrl { get; set; } // الرابط (Cloud/Local)

        [MaxLength(50)]
        public required string FileType { get; set; } // الامتداد (.jpg, .dicom)

        public long FileSizeInBytes { get; set; } // الحجم (لإدارة المساحة)

        // ==========================================
        // 3. Medical Context (السياق الطبي)
        // ==========================================

        public MedicalAttachmentType AttachmentType { get; set; } // (أشعة، تحليل، تقرير)

        [MaxLength(500)]
        public string? Description { get; set; } // وصف (مثال: "أشعة مقطعية قبل العملية")

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        // ==========================================
        // 4. AI Integration (تجهيز للمستقبل)
        // ==========================================

        // هل تم تحليله؟
        public bool IsAnalyzedByAI { get; set; } = false;

        // النتيجة الخام (JSON) - إحداثيات أو داتا معقدة
        public string? AIAnalysisResultJson { get; set; }

        // [مهم]: ملخص نصي من الـ AI (عشان يظهر للدكتور في الـ UI مباشرة)
        // مثال: "Normal Chest X-Ray" أو "Possible fracture detected"
        public string? AISummary { get; set; }
    }
}