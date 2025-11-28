using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nabd.Core.Enums;
namespace Nabd.Core.Entities
{
    // abstract: عشان ممنوع حد ياخد منه نسخة مباشرة، لازم يورث منه
    public abstract class BaseEntity
    {
        // 1. المفتاح الأساسي (Standard Guid)
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // ==========================================
        // 2. Auditing (التدقيق ومراقبة البيانات)
        // ==========================================

        // متى تم الإنشاء؟
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        // مين اللي أنشأه؟ (User ID)
        public string? CreatedBy { get; set; }

        // متى آخر تعديل؟
        public DateTime? LastModifiedOn { get; set; }

        // مين آخر واحد عدل؟
        public string? LastModifiedBy { get; set; }

        // ==========================================
        // 3. Soft Delete (الحذف المنطقي)
        // ==========================================

        // هل تم حذفه؟ (بدل ما نمسح السطر من الداتابيز)
        public bool IsDeleted { get; set; } = false;

        // مين حذفه؟
        public string? DeletedBy { get; set; }

        // اتحذف امتى؟
        public DateTime? DeletedOn { get; set; }

        // ==========================================
        // 4. Advanced: Concurrency (منع التضارب)
        // ==========================================

        // الخاصية دي سحرية: لو اتنين عدلوا في نفس الوقت، الداتابيز هترفض التاني
        // ده بيمنع مشكلة الـ Data Overwrite
        [Timestamp]
        public required byte[] RowVersion { get; set; }

        // ==========================================
        // 5. Domain Events (للمستقبل)
        // ==========================================

        // دي قائمة أحداث مش بتتسجل في الداتابيز، لكن بنستخدمها في الكود
        // مثال: بمجرد ما المريض يتحجز (Event)، ابعتله إيميل
        [NotMapped]
        public List<object> DomainEvents { get; private set; } = new List<object>();

        // دالة لإضافة حدث
        public void AddDomainEvent(object domainEvent)
        {
            DomainEvents.Add(domainEvent);
        }

        // دالة لمسح الأحداث بعد تنفيذها
        public void ClearDomainEvents()
        {
            DomainEvents.Clear();
        }
    }
}