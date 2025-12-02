using Microsoft.EntityFrameworkCore;
using Nabd.Core.Interfaces;
using Nabd.Core.Specifications;
using Nabd.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Nabd.Core.Entities.Base;

namespace Nabd.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. عمليات القراءة (Read Operations)
        // ==========================================

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            // AsNoTracking: تحسين أداء رهيب للقوائم (لأننا مش هنعدل عليها هنا)
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        // جلب عنصر واحد بالشروط (Specification)
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            // بنطبق الشروط ونرجع أول واحد
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        // جلب قائمة بالشروط (Specification)
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            // بنطبق الشروط، وبنستخدم AsNoTracking ضمناً داخل الـ Query لو حبينا
            return await ApplySpecification(spec).ToListAsync();
        }

        // عد العناصر بالشروط (مهم جداً للـ Pagination في الفرونت إند)
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        // ==========================================
        // 2. عمليات الكتابة (Write Operations)
        // ==========================================
        // ملاحظة: مفيش SaveChanges هنا، الـ UnitOfWork هو اللي بيعمل Commit في الآخر

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            // Enterprise Rule: Soft Delete
            // في الأنظمة الطبية ممنوع نمسح داتا، بنعملها "أرشفة" بتغيير الحالة
            entity.IsDeleted = true;
            Update(entity);
        }

        // ==========================================
        // 3. المطبخ الداخلي (Helper Method)
        // ==========================================

        // الدالة دي بترجم الـ Specification لـ SQL Query
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
    }
}