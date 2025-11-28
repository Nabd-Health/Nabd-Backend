using System;
using System.ComponentModel.DataAnnotations;
using Nabd.Core.Enums; // Required for HistoryEventType Enum

namespace Nabd.Application.DTOs.Patient
{
    // هذا الكلاس يمثل عنصراً واحداً في الخط الزمني للسجل الطبي للمريض
    public class PatientHistoryItemDto
    {
        // ==================================================
        // 1. Identification & Time
        // ==================================================

        // ID السجل الأصلي (Appointment ID, Lab ID, etc.)
        public required Guid Id { get; set; }

        public required DateTime EventDate { get; set; }

        // ==================================================
        // 2. Event Context (المحتوى والنوع)
        // ==================================================

        // نوع الحدث (زيارة، تحليل، تنبيه) - (جدولنا الجديد في Enums)
        public required HistoryEventType EventType { get; set; }

        [Required]
        // العنوان الذي يظهر في الـ Timeline (مثال: "كشف مع د. خالد")
        public required string Title { get; set; }

        public string? Details { get; set; } // ملخص التشخيص أو الشكوى

        // ==================================================
        // 3. Clinical & Audit Data
        // ==================================================

        public string? Diagnosis { get; set; } // التشخيص النهائي المعتمد في هذه الزيارة

        public string? DoctorName { get; set; } // الطبيب الذي قام بالكشف

        public bool HasPrescription { get; set; } = false; // هل يوجد روشتة مرتبطة بهذا الكشف؟

        public bool IsCritical { get; set; } = false; // flag لإظهار تنبيه في الـ Timeline (مثال: زيارة طوارئ)

        // رابط لملف الـ PDF أو الصورة (لو كان تحليل أو أشعة)
        public Guid? SourceRecordId { get; set; }
    }
}