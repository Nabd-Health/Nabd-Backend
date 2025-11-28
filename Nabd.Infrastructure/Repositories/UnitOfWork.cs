using Nabd.Core.Entities;
using Nabd.Core.Interfaces;
using Nabd.Infrastructure.Data;
using System.Collections;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nabd.Infrastructure.Repositories
{
    // Implementation of the Unit of Work pattern using Caching for repositories
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // المخزن: يحفظ الـ Repositories التي تم إنشاؤها بالفعل (Cashing)
        private Hashtable _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. Transaction Management (الإطلاق)
        // ==========================================

        // زر الحفظ النهائي: يتم استدعاؤه مرة واحدة لضمان حفظ كل التعديلات (Add/Update/Delete) في نفس اللحظة (Transaction)
        public async Task<int> CompleteAsync()
        {
            // هنا يتم أيضاً تنفيذ أي Domain Events قمنا بتسجيلها في BaseEntity (للعمليات المتقدمة)
            return await _context.SaveChangesAsync();
        }

        // تنظيف الذاكرة (Memory Cleanup)
        public void Dispose()
        {
            _context.Dispose();
        }

        // ==========================================
        // 2. Dynamic Repository Resolver (الخلق الديناميكي)
        // ==========================================

        // الدالة السحرية: تطلب منها نوع كيان (TEntity)، فترجع لك الريبو المخصص له
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            // التأكد من أن المخزن قد تم إنشاؤه
            if (_repositories == null)
                _repositories = new Hashtable();

            // الحصول على اسم الكيان (مثال: Doctor) ليكون هو المفتاح في القاموس
            var type = typeof(TEntity).Name;

            // إذا لم يتم إنشاء الريبو من قبل، نقوم بإنشائه الآن
            if (!_repositories.ContainsKey(type))
            {
                // استخدام Reflection لإنشاء GenericRepository<TEntity>
                var repositoryType = typeof(GenericRepository<>);

                // هنا بنقول للبرنامج: اخلق لي نسخة من GenericRepository وحدد نوعها بـ TEntity اللي جاية
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    _context // ونمرر له الـ DbContext
                );

                // حفظ الريبو في القاموس لتسريعه في المرات القادمة
                _repositories.Add(type, repositoryInstance);
            }

            // إرجاع الريبو بعد تحويل نوعه إلى IGenericRepository<TEntity>
            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}