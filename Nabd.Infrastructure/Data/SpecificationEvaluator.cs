using Microsoft.EntityFrameworkCore;
using Nabd.Core.Entities;
using Nabd.Core.Specifications; // تأكد إنك عامل الـ Namespace ده في Core

namespace Nabd.Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            // 1. تطبيق الفلترة (Where)
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            // 2. تطبيق الترتيب (Order By)
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if (spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            // 3. تطبيق التصفح (Pagination)
            if (spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            // 4. تطبيق الربط (Includes) - عشان نجيب الجداول المرتبطة
            // السطر ده عبقري: بيلف على كل الـ Includes اللي طلبتها ويضيفها للكويري
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}