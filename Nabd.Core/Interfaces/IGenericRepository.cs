using Nabd.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Nabd.Core.Entities.Base; // ⬅️ هذا هو السطر الحاسم لحل CS0308 // ⚠️ ده السطر اللي كان ناقص (مهم للـ Specifications)

namespace Nabd.Core.Interfaces
{
    // لازم نحدد T هنا (لأنها بتورث من BaseEntity)
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();

        // دوال الـ Specifications الجديدة
        Task<T> GetEntityWithSpec(ISpecification<T> spec); // CS0535: ده كان السبب
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}