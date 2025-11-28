using Nabd.Core.Entities;
using Nabd.Core.Specifications;
using System.Linq.Expressions;

namespace Nabd.Core.Specifications
{
    // هذا الكلاس يحدد طريقة البحث عن الـ RefreshToken في الداتابيز
    public class RefreshTokenByTokenSpec : BaseSpecification<RefreshToken>
    {
        // الكونستركتور يبحث عن التوكن بناءً على القيمة المشفرة
        public RefreshTokenByTokenSpec(string token)
            : base(t => t.Token == token)
        {
            // لا حاجة لإضافة أي شيء هنا
        }

        // كونستركتور للبحث عن التوكن النشط فقط
        public RefreshTokenByTokenSpec(string token, bool onlyActive)
            : base(t => t.Token == token && t.IsActive == onlyActive)
        {
            // نضمن أننا نبحث فقط عن الكيانات التي لم تحذف منطقياً
            // هذا يعتمد على وجود خاصية IsDeleted في BaseSpecification
            AddCriteria(t => t.IsDeleted == false);
        }
    }
}