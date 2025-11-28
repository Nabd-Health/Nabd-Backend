using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nabd.Core.Entities;

namespace Nabd.Core.Interfaces
{
    // IDisposable: عشان ننظف الذاكرة بعد ما الريكويست يخلص
    public interface IUnitOfWork : IDisposable
    {
        // دالة سحرية: بتطلب منها أي جدول، ترجعلك الريبو بتاعه فوراً
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        // زرار الحفظ النهائي (Commit Transaction)
        Task<int> CompleteAsync();
    }
}