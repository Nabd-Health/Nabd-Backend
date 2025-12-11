using System.Linq.Expressions;
using System.Collections.Generic;
using Nabd.Core.Entities.Base;

namespace Nabd.Core.Specifications
{
    
    public abstract class BaseSpecification<T> : ISpecification<T> where T : class
    {
        // ==========================================
        // 1.(Properties) 
        // ==========================================


        
        public Expression<Func<T, bool>> Criteria { get; private set; } = default!;

      
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

       
        public Expression<Func<T, object>> OrderBy { get; private set; } = default!;
        public Expression<Func<T, object>> OrderByDescending { get; private set; } = default!;

       
        public Expression<Func<T, object>>? GroupBy { get; private set; }

        // Paging
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        // ==========================================
        // 2. Constructors 
        // ==========================================

       
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

      
        protected BaseSpecification() { }


        // ==========================================
        // 3. Helper 
        // ==========================================

     
        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

    
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

    
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }


        protected void ApplyGrouping(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}