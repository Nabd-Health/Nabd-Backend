using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Nabd.Core.Entities.Base; // نعتمد على BaseEntity

namespace Nabd.Core.Interfaces.Repositories.Base
{
    // T يجب أن يكون من نوع كلاس، والأفضل أن يرث من BaseEntity لضمان وجود Id و Auditing
    public interface IGenericRepository<T> where T : BaseEntity // تعديل: لزيادة دقة الموديل
    {
        // ==========================================
        // I. Read Operations (الاستعلام)
        // ==========================================

        // 1. استرجاع كائن بواسطة الهوية (ID)
        Task<T?> GetByIdAsync(Guid id);

        // 2. استرجاع جميع الكائنات
        Task<IEnumerable<T>> GetAllAsync();

        // 3. استرجاع عدد السجلات (مع إمكانية الفلترة)
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

        // 4. استرجاع كائن واحد بناءً على شرط (مع تحميل العلاقات باستخدام includeProperties)
        Task<T?> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = "");

        // 5. استرجاع جميع الكائنات بناءً على شرط (Predicate)
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        // 6. التحقق من وجود سجل
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);

        // 7. الحصول على Queryable (للاستعلامات المعقدة خارج نطاق الريبوزيتوري)
        IQueryable<T> GetQueryable();

        // ==========================================
        // II. Write Operations (الكتابة)
        // ==========================================

        // 1. إضافة كائن جديد
        Task<T> AddAsync(T entity);

        // 2. تحديث كائن موجود
        void Update(T entity);

        // 3. حذف كائن (Soft Delete أو Hard Delete حسب Entity)
        void Delete(T entity);

        // [تعديل] حذف بواسطة الهوية (ID)
        Task DeleteByIdAsync(Guid id);

        // [ميزة إضافية]: إضافة قائمة من الكائنات (Batch Add)
        Task AddRangeAsync(IEnumerable<T> entities);
    }
}