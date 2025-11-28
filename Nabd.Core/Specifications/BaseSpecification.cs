using Nabd.Core.Entities;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Nabd.Core.Specifications
{
    // هذا الكلاس هو القالب الذي سترث منه كل الاستعلامات المحددة (Specific Queries)
    public abstract class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        // ==========================================
        // 1. الخصائص (Properties) التي تنفذ عقد ISpecification
        // ==========================================

        // Criteria: شرط الفلترة (WHERE) - مسموح بـ null لأنه اختياري
        public Expression<Func<T, bool>>? Criteria { get; private set; }

        // Includes: قائمة بالجداول المراد ربطها (Eager Loading)
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        // Ordering: الترتيب (مسموح بـ null)
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        // Grouping: للتجميع في التقارير
        public Expression<Func<T, object>>? GroupBy { get; private set; }

        // Paging: التصفح
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        // ==========================================
        // 2. Constructors (دوّال البناء)
        // ==========================================

        // الكونستركتور الأساسي (يستخدم عندما يكون الشرط إجباري)
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        // الكونستركتور الفارغ (يستخدم عندما نريد جلب كل شيء)
        protected BaseSpecification() { }


        // ==========================================
        // 3. دوال الـ Helper (لتكوين الكويري)
        // ==========================================

        // دالة لإضافة شرط الفلترة (يتم استدعاؤها في الكونستركتور)
        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

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

        // دالة لتحديد الترتيب الهابط
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