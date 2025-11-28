using System.Linq.Expressions;
using Nabd.Core.Entities;

namespace Nabd.Shared.Specifications
{
    // هذا الكلاس هو القالب الذي سترث منه كل الاستعلامات المحددة (Specific Queries)
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        // 1. الخصائص (Properties) التي تنفذ عقد ISpecification

        // Criteri: هي الشرط (WHERE) الذي يتم تمريره للكويري
        public Expression<Func<T, bool>> Criteria { get; private set; }

        // Includes: قائمة بالجداول المراد ضمها (Eager Loading)
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        // Ordering: الترتيب (ASC)
        public Expression<Func<T, object>> OrderBy { get; private set; }

        // Ordering: الترتيب (DESC)
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        // Grouping: للتجميع في التقارير (Future Proofing)
        public Expression<Func<T, object>> GroupBy { get; private set; }


        // Paging: التصفح
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        // ==========================================
        // 2. الكونستركتور (لإضافة شرط الفلترة الأساسي)
        // ==========================================

        // الكونستركتور الأساسي: يستخدم عندما يكون الشرط إجباري (مثل GetById)
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        // الكونستركتور الفارغ: يستخدم عندما نريد جلب كل شيء (مثل GetAll)
        protected BaseSpecification() { }


        // ==========================================
        // 3. دوال الـ Helper (لتكوين الكويري)
        // ==========================================

        // دالة لإضافة جداول مرتبطة (JOINs)
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        // دالة لتحديد الترتيب الصاعد
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        // دالة لتحديد الترتيب الهابط (الأحدث أولاً)
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        // دالة لتفعيل التصفح
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        // دالة لتفعيل التجميع (Grouping)
        protected void ApplyGrouping(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}