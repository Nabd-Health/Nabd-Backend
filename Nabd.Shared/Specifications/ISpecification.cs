using System.Linq.Expressions;
using System.Collections.Generic;

namespace Nabd.Shared.Specifications
{
    // هذا العقد يحدد كل ما يمكن أن يفعله الاستعلام (Query)
    public interface ISpecification<T>
    {
        // ==========================================
        // 1. Filtering (التصفية - تقابل جملة WHERE في SQL)
        // ==========================================
        // Expression: هي الطريقة التي يترجم بها C# جملة (x => x.Id == 5) إلى SQL
        Expression<Func<T, bool>> Criteria { get; }

        // ==========================================
        // 2. Related Entities (الربط - تقابل جملة JOIN في SQL)
        // ==========================================
        // قائمة بأسماء الجداول الأخرى المراد تحميلها مع الكيان الرئيسي
        // مثال: عند طلب Doctor، أحضر معه Appointments الخاصة به
        List<Expression<Func<T, object>>> Includes { get; }

        // ==========================================
        // 3. Ordering (الترتيب - تقابل جملة ORDER BY)
        // ==========================================
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        // ==========================================
        // 4. Grouping (التجميع - لتقارير المستقبل)
        // ==========================================
        Expression<Func<T, object>> GroupBy { get; }


        // ==========================================
        // 5. Paging (التصفح - مهم للـ Frontend)
        // ==========================================
        int Take { get; } // عدد العناصر المراد جلبها في الصفحة الواحدة
        int Skip { get; } // عدد العناصر التي سيتم تخطيها (للصفحة رقم 2، 3، إلخ)
        bool IsPagingEnabled { get; } // هل هذا الاستعلام يدعم التصفح؟
    }
}